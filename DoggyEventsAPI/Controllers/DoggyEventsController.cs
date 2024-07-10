using DoggyEvents.Models.Dtos;
using DoggyEvents.Models.Models;
using DoggyEvents.DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DoggyEventsAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DoggyEventsController : ControllerBase
  {
    private readonly IEventCategoryRepository _eventCategoryRepository;
    private readonly IDoggyEventRepository _doggyEventRepository;


    //use constructor injection for the repos
    public DoggyEventsController(IDoggyEventRepository doggyEventsRepository, IEventCategoryRepository eventCategoryRepository)
    {
      this._eventCategoryRepository = eventCategoryRepository;
      this._doggyEventRepository = doggyEventsRepository;
    }

    //POST: {apibaseurl}/api/doggyevents
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateDoggyEvent([FromBody] CreateDoggyEventRequestDto doggyEventCreateDto)
    {
      // Convert DTO to Domain Model
      var doggyEvent = new DoggyEvent
      {
        DogName = doggyEventCreateDto.DogName,
        PublishedDate = doggyEventCreateDto.PublishedDate,
        EventCategories = new List<EventCategory>()
      };

      foreach (var categoryGuid in doggyEventCreateDto.EventCategories)
      {
        var existingCategory = await _eventCategoryRepository.GetById(categoryGuid);
        if (existingCategory is not null)
        {
          doggyEvent.EventCategories.Add(existingCategory);
        }
      }

      doggyEvent = await _doggyEventRepository.CreateAsync(doggyEvent);

      //Domain Model to Dto
      var response = new DoggyEventDto
      {
       Id = doggyEvent.Id,
       DogName = doggyEvent.DogName,
       PublishedDate = doggyEvent.PublishedDate,
       EventCategories = doggyEvent.EventCategories.Select(x => new EventCategoryDto
        {
          Id = x.Id,
          Name = x.Name
        }).ToList()
      };
      return Ok(response);

    }


  }
}
