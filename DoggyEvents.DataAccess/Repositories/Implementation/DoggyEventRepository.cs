using DoggyEvents.DataAccess.Data;
using DoggyEvents.DataAccess.Repositories.Interface;
using DoggyEvents.Models.Models;
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
  }
}
