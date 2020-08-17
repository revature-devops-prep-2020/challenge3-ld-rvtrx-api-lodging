using System.Threading.Tasks;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork
  {
    private readonly LodgingContext _context;

    public virtual LodgingRepo Lodging { get; }
    public virtual Repository<RentalModel> Rental { get; set; }
    public virtual Repository<ReviewModel> Review { get; set; }

    public UnitOfWork(LodgingContext context)
    {
        _context = context;

        Lodging = new LodgingRepo(context);
        Rental = new Repository<RentalModel>(context);
        Review = new Repository<ReviewModel>(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
