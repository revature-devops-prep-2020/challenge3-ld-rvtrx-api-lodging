using System.Threading.Tasks;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.ObjectModel.Interfaces
{
  public interface IUnitOfWork
  {
    ILodgingRepo Lodging { get; }
    IRepository<RentalModel> Rental { get; set; }
    IRepository<ReviewModel> Review { get; set; }

    Task<int> CommitAsync();
  }
}
