using DoggyEvents.Models.Dtos;
using DoggyEvents.Models.Models;
using DoggyEvents.DataAccess.Repositories.Interface;
using DoggyEvents.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoggyEventsAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventCategoriesController : ControllerBase
  {
    private readonly IEventCategoryRepository _eventCategoryRepository;


    //use constructor injection for the repos
    public EventCategoriesController(IEventCategoryRepository eventCategoryRepository)
    {
      this._eventCategoryRepository = eventCategoryRepository;
    }


    //POST: {apibaseurl}/api/eventcategories
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateEventCategory(CreateEventCategoryRequestDto eventCategoryCreateDto)
    {
      // Convert DTO to Domain Model
      EventCategory category = new EventCategory
      {
        Name = eventCategoryCreateDto.Name
      };
      category = await _eventCategoryRepository.CreateAsync(category);

      //Domain Model to Dto
      var response = new EventCategoryDto
      {
        Id = category.Id,
        Name = category.Name
      };
      return Ok(response);

    }


  }
}
