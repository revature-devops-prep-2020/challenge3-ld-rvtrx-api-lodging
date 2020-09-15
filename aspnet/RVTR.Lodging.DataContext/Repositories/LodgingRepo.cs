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
    /// This method will return all the lodgings in the given location whose rental status is "available" and where occupancy is not less than the 
    /// desired occupancy. It will include the Rentals, Location, and Address tables in its non-case-sensitive filter action. Null or empty fields 
    /// for City, State/Province, or Country are ignored. 
    /// </summary>
    public async Task<IEnumerable<LodgingModel>> LodgingByLocationAndOccupancy(string city, string state, string country, int occupancy)
    {
      var lodgingsByLocation = await _db
        .Include(r => r.Rentals)
        .Include(l => l.Location)
        .Include(a => a.Location.Address)
        .Where(c => string.IsNullOrEmpty(city) ? true : c.Location.Address.City.ToLower() == city.ToLower())
        .Where(s => string.IsNullOrEmpty(state) ? true : s.Location.Address.StateProvince.ToLower() == state.ToLower())
        .Where(s => string.IsNullOrEmpty(country) ? true : s.Location.Address.Country.ToLower() == country.ToLower())
        .ToListAsync();

      var filteredLodgings = new List<LodgingModel>();

      foreach (var item in lodgingsByLocation)
      {
        foreach (var rental in item.Rentals)
        {
          if (rental.Status.Equals("available") && rental.Occupancy >= occupancy)
          {
            if(!filteredLodgings.Contains(item))
            {
              filteredLodgings.Add(item);
            }
          }
        }
      }
      return filteredLodgings;
    }
  }
}
