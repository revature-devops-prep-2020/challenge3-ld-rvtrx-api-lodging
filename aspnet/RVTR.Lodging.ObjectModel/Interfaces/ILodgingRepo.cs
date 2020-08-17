using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.ObjectModel.Interfaces
{
  public interface ILodgingRepo
  {
    Task<IEnumerable<LodgingModel>> AvailableLodgings();
        Task DeleteAsync(int id);
    }
}
