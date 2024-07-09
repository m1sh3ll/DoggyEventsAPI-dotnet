using System.ComponentModel.DataAnnotations;

namespace DoggyEvents.Models.Models
{
  public class EventCategory
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<DoggyEvent> DoggyEvents { get; set; }
  }
}
