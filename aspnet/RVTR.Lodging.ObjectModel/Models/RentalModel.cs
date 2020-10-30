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
    [MaxLength(5, ErrorMessage = "Lot number must be 5 digits maximum")]
    [RegularExpression(@"^[0-9]?[0-9]?[0-9]?[0-9]?[0-9]$")]
    public string LotNumber { get; set; }

    public RentalUnitModel Unit { get; set; }

    [Required(ErrorMessage = "Status can't be null.")]
    [RegularExpression(@"^([Bb]ooked|[Aa]vailable)$", ErrorMessage = "Status must be either 'Booked' or 'Available'")]
    public string Status { get; set; }

    [Required(ErrorMessage = "Price can't be null.")]
    [RegularExpression(@"^\d+(\.\d\d)?$", ErrorMessage = "Price must be in the form 00.00")]
    public double Price { get; set; }

    [RegularExpression(@"^\d+(\.\d\d)?$", ErrorMessage = "Price must be in the form 00.00")]
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
