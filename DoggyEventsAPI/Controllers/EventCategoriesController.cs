using DoggyEvents.Models.Dtos;
using DoggyEvents.Models.Models;
using DoggyEvents.DataAccess.Repositories.Interface;
using DoggyEvents.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoggyEventsAPI.Controllers
{
//{apibaseurl}/api/eventcategories
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
    //[Authorize]
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


    //GET: {apibaseurl}/api/eventcategories
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
      var categories = await _eventCategoryRepository.GetAllAsync();

      var response = new List<EventCategoryDto>();
      // map domain to dto
      foreach (var category in categories)
      {
        response.Add(new EventCategoryDto
        {
          Id = category.Id,
          Name = category.Name
        });
      }
      return Ok(response);
    }

    //GET: {apibaseurl}/api/eventcategories/id
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
    {
      var category = await _eventCategoryRepository.GetById(id);

      if (category is null)
      {
        return NotFound();
      }

      var response = new EventCategoryDto
      {
        Id = category.Id,
        Name = category.Name
      };
      return Ok(response);
    }



  }
}
