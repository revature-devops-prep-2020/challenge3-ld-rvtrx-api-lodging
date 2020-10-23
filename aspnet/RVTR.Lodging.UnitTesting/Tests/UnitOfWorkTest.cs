using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class UnitOfWorkTest : DataTest
  {
    [Fact]
    public async void Test_UnitOfWork_CommitAsync()
    {
      using var ctx = new LodgingContext(Options);
      var unitOfWork = new UnitOfWork(ctx);
      var actual = await unitOfWork.CommitAsync();

      Assert.NotNull(unitOfWork.Lodging);
      Assert.NotNull(unitOfWork.Rental);
      Assert.NotNull(unitOfWork.Review);
      Assert.Equal(0, actual);
    }
  }
}
