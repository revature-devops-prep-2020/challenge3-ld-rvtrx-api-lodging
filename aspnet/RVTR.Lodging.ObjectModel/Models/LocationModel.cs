using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Location_ model
  /// </summary>
  public class LocationModel : IValidatableObject
  {
    /// <summary>
    /// Id for the location model
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Id of the address property
    /// </summary>
    /// <value></value>
    public int AddressId { get; set; }

    /// <summary>
    /// Address property for the location model
    /// </summary>
    /// <value></value>
    public AddressModel Address { get; set; }

    /// <summary>
    /// Latitude of the location model
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Latitude is required")]
    [RegularExpression(@"^[+-]?([0-8]?\d(\.\d+)?|90(\.0+)?)$", ErrorMessage = "Latitude must be in the form 00.000 or -00.000")]
    public string Latitude { get; set; }

    /// <summary>
    /// Longitude of the location model, validated to be in the format +-00.000, max of 180, min of 0
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Longitude is required")]
    [RegularExpression(@"^[+-]?(1([0-7]\d(\.\d+)?|80(\.0+)?)|(\d{2}|\d)(\.\d+))$", ErrorMessage = "Longitude must be in the form 000.0000 or -000.000")]
    public string Longitude { get; set; }

    /// <summary>
    /// Represents the _Location_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
