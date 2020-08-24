using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class AddressModelTest
  {
    public static readonly IEnumerable<object[]> _addresses = new List<object[]>
    {
      new object[]
      {
        new AddressModel()
        {
          Id = 0,
          City = "city",
          Country = "country",
          PostalCode = "postalcode",
          StateProvince = "stateprovince",
          Street = "street",
          LocationId = 0,
          Location = null
        }
      }
    };

    [Theory]
    [MemberData(nameof(_addresses))]
    public void Test_Create_AddressModel(AddressModel address)
    {
      var validationContext = new ValidationContext(address);
      var actual = Validator.TryValidateObject(address, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_addresses))]
    public void Test_Validate_AddressModel(AddressModel address)
    {
      var validationContext = new ValidationContext(address);

      Assert.Empty(address.Validate(validationContext));
    }
  }
}
