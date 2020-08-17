using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingModelTest
  {
    public static readonly IEnumerable<Object[]> _lodgings = new List<Object[]>
    {
      new object[]
      {
        new LodgingModel()
        {
          Id = 0,
          Location = new LocationModel(),
          Name = "name",
          Bathrooms = 0,
          Rentals = new List<RentalModel>(),
          Reviews = new List<ReviewModel>()
        }
      }
    };

    [Theory]
    [MemberData(nameof(_lodgings))]
    public void Test_Create_LodgingModel(LodgingModel lodging)
    {
      var validationContext = new ValidationContext(lodging);
      var actual = Validator.TryValidateObject(lodging, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_lodgings))]
    public void Test_Validate_LodgingModel(LodgingModel lodging)
    {
      var validationContext = new ValidationContext(lodging);

      Assert.Empty(lodging.Validate(validationContext));
    }
  }
}
