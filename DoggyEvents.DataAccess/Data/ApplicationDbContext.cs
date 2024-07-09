using DoggyEvents.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DoggyEvents.DataAccess.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<EventCategory> EventCategories { get; set; } 

    public DbSet<DoggyEvent> DoggyEvents { get; set; }  
  }
}
