using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Address_ model
  /// </summary>
  public class AddressModel : IValidatableObject
  {
    /// <summary>
    /// Id for the address model
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// City property must have a capitalized first initial
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "City is required")]
    [MinLength(1, ErrorMessage = "Name must be at least one character.")]
    [MaxLength(20, ErrorMessage = "Name must be fewer than 20 characters.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only uses letters.")]
    public string City { get; set; }

    /// <summary>
    /// Country property must be US or USA
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Country is required")]
    [RegularExpression(@"(^USA$)|(^US$)", ErrorMessage = "Country must be 'US' or 'USA'")]
    public string Country { get; set; }

    /// <summary>
    /// Location property for the address model
    /// </summary>
    /// <value></value>
    public LocationModel Location { get; set; }

    /// <summary>
    /// Postal code must be 5 digits between 0 and 9
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Postal code is required")]
    [StringLength(5, ErrorMessage = "Postal code must be 5 numbers long")]
    [RegularExpression(@"\d{5}", ErrorMessage = "Postal code must be a number")]
    public string PostalCode { get; set; }

    /// <summary>
    /// Restricts state to its capitalized two letter abbreviation
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "State is required")]
    [StringLength(2, ErrorMessage = "State must be 2 characters long")]
    [RegularExpression(@"[A-Z]{2}", ErrorMessage = "State must be abbreviated properly.")]
    public string StateProvince { get; set; }

    /// <summary>
    /// Street must be between 1 and 50 characters long
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Street is required")]
    [MinLength(1)]
    [MaxLength(200, ErrorMessage = "Street name is too long.")]
    public string Street { get; set; }

    /// <summary>
    /// Represents the _Address_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
