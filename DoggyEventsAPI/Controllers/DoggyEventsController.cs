using DoggyEvents.Models.Dtos;
using DoggyEvents.Models.Models;
using DoggyEvents.DataAccess.Repositories.Interface;
using DoggyEvents.DataAccess.Repositories.Implementation;
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
    //[Authorize]
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


    //GET: {apibaseurl}/api/doggyevents
    [HttpGet]
    public async Task<IActionResult> GetAllDoggyEvents()
    {
      var dogEvents = await _doggyEventRepository.GetAllAsync();

      //Convert Domain Model to DTO
      var response = new List<DoggyEventDto>();

      foreach (var doggyEvent in dogEvents)
      {
        response.Add(new DoggyEventDto
        {
          Id = doggyEvent.Id,
          DogName = doggyEvent.DogName,          
          PublishedDate = doggyEvent.PublishedDate,
          EventCategories = doggyEvent.EventCategories.Select(x => new EventCategoryDto
          {
            Id = x.Id,
            Name = x.Name
          }).ToList()
        });
      }
      return Ok(response);
    }




    //GET: {apibaseurl}/api/doggyevents/{id}
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetDoggyEventById([FromRoute] Guid id) 
    {
      // Get the blog post from the repository
      var dogEvent = await _doggyEventRepository.GetByIdAsync(id);

      if (dogEvent is null)
      {
        return NotFound();
      }

      //Convert domain model to DTO
      var response = new DoggyEventDto
      {
        Id = dogEvent.Id,
        DogName = dogEvent.DogName,
        PublishedDate = dogEvent.PublishedDate,
        EventCategories = dogEvent.EventCategories.Select(x => new EventCategoryDto
        {
          Id = x.Id,
          Name = x.Name
        }).ToList()
      };
      return Ok(response);
    }



    //DELETE: {apibaseurl}/api/doggyevents/{id}
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteDoggyEvent([FromRoute] Guid id)
    {
      var deletedDogEvent = await _doggyEventRepository.DeleteAsync(id);

      if (deletedDogEvent is null)
      {
        return NotFound();
      }
      //convert domain to dto
      var response = new DoggyEventDto
      {
        Id = deletedDogEvent.Id,
        DogName = deletedDogEvent.DogName,        
        PublishedDate = deletedDogEvent.PublishedDate    

      };
      return Ok(response);
    }



    //PUT: {apibaseurl}/api/doggyevents/{id}
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateDoggyEvent([FromRoute] Guid id, UpdateDoggyEventDto updateDoggyEventDto)
    {
      //Convert DTO to Domain Model
      var dogEvent = new DoggyEvent
      {
        Id = id,
        DogName = updateDoggyEventDto.DogName,       
        PublishedDate = updateDoggyEventDto.PublishedDate,        
        EventCategories = new List<EventCategory>()
      };

      foreach (var categoryGuid in updateDoggyEventDto.EventCategories)
      {
        var existingCategory = await _eventCategoryRepository.GetById(categoryGuid);

        if (existingCategory is not null)
        {
          dogEvent.EventCategories.Add(existingCategory);
        }
      }
      //Call Repository to Update BlogPost Domain Model
      var updatedBlogPost = await _doggyEventRepository.UpdateAsync(dogEvent);
      if (updatedBlogPost is null)
      {
        return NotFound();
      }

      //Convert Domain Model back to DTO
      var response = new DoggyEventDto
      {
        Id = dogEvent.Id,
        DogName = updateDoggyEventDto.DogName,        
        PublishedDate = updateDoggyEventDto.PublishedDate,        
        EventCategories = dogEvent.EventCategories.Select(x => new EventCategoryDto
        {
          Id = x.Id,
          Name = x.Name
        }).ToList()
      };

      return Ok(response);
    }
















  }
}
