using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoggyEvents.Models.Dtos
{
  public class UpdateDoggyEventDto
  {
    public string DogName { get; set; }

    public DateTime PublishedDate { get; set; }

    public List<Guid> EventCategories { get; set; } = new List<Guid>();
  }
}
