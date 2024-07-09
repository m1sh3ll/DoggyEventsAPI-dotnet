using DoggyEvents.DataAccess.Data;
using DoggyEvents.DataAccess.Repositories.Interface;
using DoggyEvents.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DoggyEvents.DataAccess.Repositories.Implementation
{
  public class EventCategoryRepository : IEventCategoryRepository
  {
    private readonly ApplicationDbContext _db;

    public EventCategoryRepository(ApplicationDbContext db)
    {
      this._db = db;
    }

    public async Task<EventCategory> CreateAsync(EventCategory eventCategory)
    {
      await _db.EventCategories.AddAsync(eventCategory);
      await _db.SaveChangesAsync();

      return eventCategory;
    }

    public async Task<EventCategory?> GetById(Guid id)
    {
      return await _db.EventCategories.FirstOrDefaultAsync(u => u.Id == id);
    }
  }
}
