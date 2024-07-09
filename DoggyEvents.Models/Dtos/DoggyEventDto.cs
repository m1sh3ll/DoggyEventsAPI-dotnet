using DoggyEvents.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoggyEvents.Models.Dtos
{
  public class DoggyEventDto
  {
    public Guid Id { get; set; }

    public string DogName { get; set; }

    public DateTime PublishedDate { get; set; }

    public List<EventCategoryDto> EventCategories { get; set; } = new List<EventCategoryDto>();

  }
}
