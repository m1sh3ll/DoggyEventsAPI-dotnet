using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoggyEvents.Models.Dtos
{
  public class CreateDoggyEventRequestDto
  {
    public string DogName { get; set; }
    public DateTime PublishedDate { get; set; }
    public Guid[] EventCategories { get; set; }
  }
}
