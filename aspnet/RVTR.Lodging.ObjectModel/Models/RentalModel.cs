using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Rental_ model
  /// </summary>
  public class RentalModel : IValidatableObject
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Lot number can't be null.")]
    [MaxLength(10, ErrorMessage = "Lot number must be 10 digits maximum")]
    [RegularExpression(@"^\d+([a-zA-Z]+)?$", ErrorMessage = "Lot number must be either a number or a number plus a series of letters.")]
    public string LotNumber { get; set; }

    public RentalUnitModel Unit { get; set; }

    [Required(ErrorMessage = "Status can't be null.")]
    [RegularExpression(@"^([Bb]ooked|[Aa]vailable)$", ErrorMessage = "Status must be either 'Booked' or 'Available'")]
    public string Status { get; set; }

    [Range(0, Double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
    public double Price { get; set; }

    [Range(0, Double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
    public double? DiscountedPrice { get; set; }

    public int? LodgingId { get; set; }

    public LodgingModel Lodging { get; set; }

    /// <summary>
    /// Represents the _Rental_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
