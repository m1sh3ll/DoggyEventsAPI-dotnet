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

   

    public async Task<IEnumerable<EventCategory>> GetAllAsync()
    {
      return await _db.EventCategories.ToListAsync();
    }


    public async Task<EventCategory?> GetById(Guid id)
    {
      return await _db.EventCategories.FirstOrDefaultAsync(u => u.Id == id);
    }



    public async Task<EventCategory?> UpdateAsync(EventCategory category)
    {
      var existingCategory = await _db.EventCategories.FirstOrDefaultAsync(x => x.Id == category.Id);

      if (existingCategory is not null)
      {
        _db.Entry(existingCategory).CurrentValues.SetValues(category);     
        await _db.SaveChangesAsync();
        return category;
      }
      return null;
    }


    public async Task<EventCategory?> DeleteAsync(Guid id)
    {
      var existingCategory = await _db.EventCategories.FirstOrDefaultAsync(x => x.Id == id);

      if (existingCategory is null)
      {
        return null;
      }

      _db.EventCategories.Remove(existingCategory);
      await _db.SaveChangesAsync();
      return existingCategory;
    }
  }
}
