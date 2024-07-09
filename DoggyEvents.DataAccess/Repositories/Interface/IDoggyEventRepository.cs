using DoggyEvents.Models.Models;


namespace DoggyEvents.DataAccess.Repositories.Interface
{
  public interface IDoggyEventRepository
  {
    public Task<DoggyEvent> CreateAsync(DoggyEvent dogEvent); 
  }
}
