using System.Collections.Generic;
using System.Threading.Tasks;

namespace RVTR.Lodging.ObjectModel.Interfaces
{
  /// <summary>
  /// Interface that provides CRUD methods for different entities depending on the given generic type
  /// </summary>
  /// <typeparam name="TEntity">The Entity which needs CRUD operations</typeparam>
  public interface IRepository<TEntity> where TEntity : class
  {
    Task DeleteAsync(int id);

    Task InsertAsync(TEntity entry);

    Task<IEnumerable<TEntity>> SelectAsync();

    Task<TEntity> SelectAsync(int id);

    void Update(TEntity entry);
  }
}
