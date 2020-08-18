using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Location_ model
  /// </summary>
  public class LocationModel : IValidatableObject
  {
    public int Id { get; set; }

        public AddressModel Address { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    /// <summary>
    /// Represents the _Location_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
