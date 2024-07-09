using Microsoft.AspNetCore.Identity;

namespace DoggyEvents.Models.Models
{
  public class ApplicationUser : IdentityUser
  {
    //Extend the default Identity user and adding a name field
    //IE.. The default "identity user" doesn't have a name field by default in its structure
    public string Name { get; set; }
  }
}
