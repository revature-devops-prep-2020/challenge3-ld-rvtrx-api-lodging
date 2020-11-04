using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.ObjectModel.Interfaces;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork : IUnitOfWork, IDisposable
  {
    private readonly LodgingContext _context;
    private bool _disposedValue;

    public ILodgingRepo Lodging { get; }
    public IRepository<RentalModel> Rental { get; set; }
    public IRepository<ReviewModel> Review { get; set; }

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

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          _context.Dispose();
        }
        _disposedValue = true;
      }
    }
    public void Dispose()
    {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
