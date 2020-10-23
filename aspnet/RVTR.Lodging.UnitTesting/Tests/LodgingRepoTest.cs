using System.Collections.Generic;
using System.Linq;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingRepoTest : DataTest
  {
    public static readonly IEnumerable<object[]> Records = new List<object[]>
    {
      new object[]
      {
        new LodgingModel
        {
          Id = 5,
          Location = new LocationModel
          {
            Id = 100, Address = new AddressModel
            {
              Id = 100, City = "Austin", StateProvince = "TX", Country = "USA"
            }
          },
          Rentals = new List<RentalModel>
          {
            new RentalModel()
            {
              Id = 100, Status = "available", Unit = new RentalUnitModel { Id = 100, Capacity = 4 }
            },
            new RentalModel()
            {
              Id = 101, Status = "booked", Unit = new RentalUnitModel { Id = 101, Capacity = 4 }
            }
          }
        }
      }
    };

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_LodgingRepo_LodgingByLocationAndOccupancy(LodgingModel lodging)
    {
      using (var ctx = new LodgingContext(Options))
      {
        await ctx.Lodgings.AddAsync(lodging);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new LodgingRepo(ctx);

        var actual = await lodgings.LodgingByLocationAndOccupancy(2, "auStin", "", null);

        Assert.NotEmpty(actual);
        Assert.True(actual.Count() == 1);
      }
    }
  }
}
