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
            var lodgings = await _db.Include(r => r.Rentals).ToListAsync();
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
            var lodgings = await _db.
                Include(r => r.Rentals).
                Include(l => l.Location).
                Include(a => a.Location.Address).ToListAsync();

            foreach(var item in lodgings)
            {
                if(item.Location.Address.City != city)
                {
                    lodgings.Remove(item);
                }
                else
                {
                    foreach (var rental in item.Rentals)
                    {
                      if (rental.Status != "available" || rental.Occupancy != occupancy)
                      {
                        lodgings.Remove(item);
                      }
                    }
                } 
            }

            return lodgings;
        }
    }
}
