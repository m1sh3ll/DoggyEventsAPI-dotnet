using DoggyEvents.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoggyEvents.Models.Dtos
{
  public  class EventCategoryUpdateDto
  {
    public string Name { get; set; }   

    public List<DoggyEvent> DoggyEvents { get; set; } 
  }
}
