using DoggyEvents.DataAccess.Data;
using DoggyEvents.DataAccess.Repositories.Interface;
using DoggyEvents.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoggyEvents.DataAccess.Repositories.Implementation
{
  public class DoggyEventRepository : IDoggyEventRepository
  {
    private readonly ApplicationDbContext _db;
    public DoggyEventRepository(ApplicationDbContext db)
    {
      this._db = db;
    }


    public async Task<DoggyEvent> CreateAsync(DoggyEvent dogEvent)
    {
      await _db.DoggyEvents.AddAsync(dogEvent);
      await _db.SaveChangesAsync();
      return dogEvent;      
    }    


    public async Task<IEnumerable<DoggyEvent>> GetAllAsync()
    {
      return await _db.DoggyEvents.Include(x => x.EventCategories).ToListAsync();
    }



    public async Task<DoggyEvent> GetByIdAsync(Guid id)
    {
      return await _db.DoggyEvents.Include(x => x.EventCategories).FirstOrDefaultAsync(x => x.Id == id);
    }



    public async Task<DoggyEvent?> UpdateAsync(DoggyEvent dogEvent)
    {
      var existingEvent = await _db.DoggyEvents.Include(x => x.EventCategories)
                                 .FirstOrDefaultAsync(x => x.Id == dogEvent.Id);

      if (existingEvent == null)
      {
        return null;
      }
      //Update Events
      _db.Entry(existingEvent).CurrentValues.SetValues(dogEvent);       

      //Update Categories
      existingEvent.EventCategories = dogEvent.EventCategories;
      await _db.SaveChangesAsync();
      return dogEvent;
    }



    public async Task<DoggyEvent?> DeleteAsync(Guid id)
    {
      var existingEvent = await _db.DoggyEvents.FirstOrDefaultAsync(x => x.Id == id);

      if (existingEvent is null)
      {
        return null;
      }
      _db.DoggyEvents.Remove(existingEvent);
      await _db.SaveChangesAsync();
      return existingEvent;
    }

  }
}
