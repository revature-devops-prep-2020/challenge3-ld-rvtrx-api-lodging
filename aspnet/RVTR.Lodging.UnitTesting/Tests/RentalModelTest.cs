using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class RentalModelTest
  {
    public static readonly IEnumerable<object[]> Rentals = new List<object[]>
    {
      new object[]
      {
        new RentalModel
        {
          Id = 0,
          Status = "Available",
          Price = 0.0,
          DiscountedPrice = 0.0,
          LodgingId = 0,
          LotNumber = "1",
          Lodging = null
        }
      }
    };

    [Theory]
    [MemberData(nameof(Rentals))]
    public void Test_Create_RentalModel(RentalModel rental)
    {
      var validationContext = new ValidationContext(rental);
      var actual = Validator.TryValidateObject(rental, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Rentals))]
    public void Test_Validate_RentalModel(RentalModel rental)
    {
      var validationContext = new ValidationContext(rental);

      Assert.Empty(rental.Validate(validationContext));
    }
  }
}
