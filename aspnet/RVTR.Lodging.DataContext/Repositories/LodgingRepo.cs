using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RVTR.Lodging.ObjectModel.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Interfaces;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class LodgingRepo : Repository<LodgingModel>, ILodgingRepo
  {

    public LodgingRepo(LodgingContext context) : base(context)
    {
    }

    public async Task<IEnumerable<LodgingModel>> AvailableLodgings()
    {
      var lodgings = await _db.ToListAsync();
      foreach (var item in lodgings)
      {
        foreach (var rental in item.Rentals)
        {
          if (rental.Status != "available")
          {
              lodgings.Remove(item);
          }
        }
      }
      return lodgings;
    }

    public async Task<IEnumerable<LodgingModel>> LodgingByCityAndOccupancy(string city, int occupancy)
    {
      return await _db.Include(r => r.Rentals
        .Where(s => s.Status == "available" && s.Occupancy == occupancy))
        .Where(c => c.Location.Address.City.Equals(city))
        .ToListAsync();
    }
  }
}
