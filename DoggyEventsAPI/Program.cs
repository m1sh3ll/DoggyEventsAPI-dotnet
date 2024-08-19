using DoggyEvents.DataAccess.Data;
using DoggyEvents.Models.Models;
using DoggyEvents.DataAccess.Repositories.Implementation;
using DoggyEvents.DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
  options.Password.RequireDigit = false;
  options.Password.RequiredLength = 1;
  options.Password.RequireLowercase = false;
  options.Password.RequireUppercase = false;
  options.Password.RequireNonAlphanumeric = false;
});


var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(u =>
{
  u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(u =>
{
  u.RequireHttpsMetadata = false;
  u.SaveToken = true;

  u.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(key)),
    ValidateIssuer = false,
    ValidateAudience = false

  };

});
builder.Services.AddCors(); //for janpreets video

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
  {
    Description = "JWT Auth header \r\n" +
    "To Login, login with the api and paste the following in the value field \r\n" +
    "Bearer andpastethetokenhere",
    Name = "Authorization",
    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    Scheme = JwtBearerDefaults.AuthenticationScheme
  });

  options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
  {
    {

      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header

      },
      new List<string>()
    }
  });
});

//Injecting the services into the services collection to be able to use the repositories in the controllers
builder.Services.AddScoped<IEventCategoryRepository, EventCategoryRepository>();
builder.Services.AddScoped<IDoggyEventRepository, DoggyEventRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); //for janpreets video
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
