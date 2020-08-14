using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.ObjectModel.Models;
using RVTR.Lodging.DataContext.Repositories;
using System.Collections.Generic;
using Xunit;
using Grpc.Core;
using System.Linq;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingRepoTests
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        new LodgingModel()
        {
          Id = 1,
          Rentals = new List<RentalModel>() { new RentalModel() {Id = 1, Status = "available" } }
        },
        new RentalModel() { Id = 1 },
        new ReviewModel() { Id = 1 }
      }
    };

    [Fact]
    public async void Test_LodgingRepo_AvailableLodgings()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new LodgingRepo(ctx);

          var actual = await lodgings.AvailableLodgings();

          Assert.Empty(actual);
          //Assert.Single(actual.ToList());
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}
