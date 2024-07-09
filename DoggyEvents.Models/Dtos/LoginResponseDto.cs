using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoggyEvents.Models.Dtos
{
  public class LoginResponseDto
  {
    public string Email { get; set; }
    public string Token { get; set; }
  }
}
