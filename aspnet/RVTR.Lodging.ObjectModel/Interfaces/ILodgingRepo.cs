using System.Collections.Generic;
using System.Threading.Tasks;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.ObjectModel.Interfaces
{
  public interface ILodgingRepo : IRepository<LodgingModel>
  {
    Task<IEnumerable<LodgingModel>> LodgingByLocationAndOccupancy(int occupancy, params string[] location);
  }
}
