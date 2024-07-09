using System.ComponentModel.DataAnnotations;

namespace DoggyEvents.Models.Models
{

//This class is for the event posts
  public class DoggyEvent
  {
    [Key]
    public Guid Id { get; set; }

    public string DogName { get; set; }

    public DateTime PublishedDate { get; set; }

    public ICollection<EventCategory> EventCategories { get; set; }   

  }
}
