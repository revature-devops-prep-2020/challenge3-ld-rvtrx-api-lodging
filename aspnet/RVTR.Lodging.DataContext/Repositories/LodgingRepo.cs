using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Interfaces;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class LodgingRepo : Repository<LodgingModel>, ILodgingRepo
  {

    public LodgingRepo(LodgingContext context) : base(context)
    {
    }

    /// <summary>
    /// This Method will select all lodgings, including their Rentals, Location, and Locations Address
    /// </summary>
    public override async Task<IEnumerable<LodgingModel>> SelectAsync() => await _db
      .Include(r => r.Rentals)
      .Include(l => l.Location)
      .ThenInclude(a => a.Address)
      .ToListAsync();

    /// <summary>
    /// This method will get a Lodging with the given Id and will include its Location and the locations address
    /// </summary>
    public override async Task<LodgingModel> SelectAsync(int id) => await _db
      .Include(r => r.Rentals)
      .Include(l => l.Location)
      .ThenInclude(a => a.Address)
      .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// This method will get all the lodgings who are located in the given city and has rentals that are available with the given occupancy.
    /// It will include the Rentals Table, Location Table, and Address Table
    /// </summary>
    public async Task<IEnumerable<LodgingModel>> LodgingByCityAndOccupancy(string city, int occupancy)
    {
      var lodgingsByCity = await _db
        .Include(r => r.Rentals)
        .Include(l => l.Location)
        .Include(a => a.Location.Address)
        .Where(b => b.Location.Address.City == city)
        .ToListAsync();

      var filteredLodgings = new List<LodgingModel>();

      foreach (var item in lodgingsByCity)
      {
        foreach (var rental in item.Rentals)
        {
          if (rental.Status.Equals("available") && rental.Occupancy == occupancy)
          {
            filteredLodgings.Add(item);
          }
        }
      }
      return filteredLodgings;
    }
  }
}
