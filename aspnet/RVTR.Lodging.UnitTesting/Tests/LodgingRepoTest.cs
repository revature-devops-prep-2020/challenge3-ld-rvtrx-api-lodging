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
          Name = "Lodging",
          Location = new LocationModel
          {
            Id = 100, Address = new AddressModel
            {
              Id = 100, City = "Austin", StateProvince = "TX", Country = "USA", PostalCode = "11111", Street = "Street"
            },
            Longitude = "1.00N",
            Latitude = "1.00W"
          },
          Rentals = new List<RentalModel>
          {
            new RentalModel()
            {
              Id = 100, LotNumber = "1", Status = "Available", Unit = new RentalUnitModel { Name = "Unit1", Size = "5x5", Id = 100, Capacity = 4 }
            },
            new RentalModel()
            {
              Id = 101, LotNumber = "2", Status = "Booked", Unit = new RentalUnitModel { Name = "Unit2", Size = "5x5", Id = 101, Capacity = 4 }
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

        var actual = await lodgings.LodgingByLocationAndOccupancy(2, "Austin");

        Assert.NotEmpty(actual);
        Assert.True(actual.Count() == 1);
      }
    }
  }
}
