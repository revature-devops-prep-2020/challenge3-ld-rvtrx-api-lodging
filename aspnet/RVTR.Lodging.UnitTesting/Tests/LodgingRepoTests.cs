using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.ObjectModel.Models;
using RVTR.Lodging.DataContext.Repositories;
using System.Collections.Generic;
using Xunit;
using Grpc.Core;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;

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
          Location = new LocationModel() {Id = 1, Address = new AddressModel() {Id = 1, City = "Austin"}},
          Rentals = new List<RentalModel>() { new RentalModel() {Id = 1, Occupancy = 3, Status = "available" } }
        }
      }
    };

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_LodgingRepo_AvailableLodgings(LodgingModel lodging)
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

          var actual = await lodgings.Lodging.AvailableLodgings();

          Assert.NotEmpty(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_LodgingRepo_LodgingByCityAndOccupancy(LodgingModel lodging)
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

          var actual = await lodgings.Lodging.LodgingByCityAndOccupancy("Austin", 3);

          Assert.NotEmpty(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}
