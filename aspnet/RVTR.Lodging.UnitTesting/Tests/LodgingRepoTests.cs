using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

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
          Id = 5,
          Location = new LocationModel()
          {
            Id = 100, Address = new AddressModel()
            {
              Id = 100, City = "Austin", StateProvince = "TX", Country = "USA"
            }
          },
          Rentals = new List<RentalModel>()
          {
            new RentalModel()
            {
              Id = 100, Status = "available", Unit = new RentalUnitModel() { Id = 100, Capacity = 4 }
            },
            new RentalModel()
            {
              Id = 101, Status = "booked", Unit = new RentalUnitModel() { Id = 101, Capacity = 4 }
            }
          }
        }
      }
    };

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_LodgingRepo_LodgingByLocationAndOccupancy(LodgingModel lodging)
    {
      await _connection.OpenAsync();
      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.Lodgings.AddAsync(lodging);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new UnitOfWork(ctx);

          var actual = await lodgings.Lodging.LodgingByLocationAndOccupancy(2, "auStin", "", null);

          Assert.NotEmpty(actual);
          Assert.True(actual.Count() == 1);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}
