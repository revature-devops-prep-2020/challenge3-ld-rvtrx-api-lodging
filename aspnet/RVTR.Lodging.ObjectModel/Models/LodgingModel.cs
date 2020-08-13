using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Lodging_ model
  /// </summary>
  public class LodgingModel : IValidatableObject
  {
    public int Id { get; set; }

    public LocationModel Location { get; set; }

    public string Name { get; set; }

    public IEnumerable<RentalModel> Rentals { get; set; }

    public IEnumerable<ReviewModel> Reviews { get; set; }

    public int Bathrooms { get; set; }

    public int CountAvailability ()
    {
      int counter = 0;
      foreach(var rental in this.Rentals)
      {
        if (rental.Status == "available")
        {
          counter += 1;
        }
      }
      return counter; 
    }

    public Dictionary<string, int> CheckLodgingTypeAvailability ()
    {
      Dictionary<string, int> rentalType = new Dictionary<string, int>();
      foreach(var rental in this.Rentals)
      {
        if (!rentalType.ContainsKey(rental.Type))
        {
          rentalType.Add(rental.Type, 0);
          if (rental.Status == "available")
          {
            rentalType[rental.Type] += 1;
          } 
        }
        else
        {
          if (rental.Status == "available")
          {
            rentalType[rental.Type] += 1;
          }
        }
      }
      return rentalType;
    }

    /// <summary>
    /// Represents the _Lodging_ `Validate` model
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
