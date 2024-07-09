using DoggyEvents.Models.Models;

namespace DoggyEvents.DataAccess.Repositories.Interface
{
  public interface IEventCategoryRepository
  {
    public Task<EventCategory> CreateAsync(EventCategory eventCategory);
    public Task<EventCategory?> GetById(Guid id);

  }
}
