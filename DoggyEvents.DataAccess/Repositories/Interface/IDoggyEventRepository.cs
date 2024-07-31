using DoggyEvents.Models.Models;


namespace DoggyEvents.DataAccess.Repositories.Interface
{
  public interface IDoggyEventRepository
  {
    public Task<DoggyEvent> CreateAsync(DoggyEvent dogEvent);

    public Task<IEnumerable<DoggyEvent>> GetAllAsync();

    public Task<DoggyEvent> GetByIdAsync(Guid id);

    public Task<DoggyEvent?> UpdateAsync(DoggyEvent dogEvent);

    public Task<DoggyEvent?> DeleteAsync(Guid id);

  }
}
