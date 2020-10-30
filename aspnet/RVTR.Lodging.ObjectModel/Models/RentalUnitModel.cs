using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  ///
  /// </summary>
  public class RentalUnitModel : IValidatableObject
  {
    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// The capacity of the rental unit
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Capacity must exist.")]
    [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000")]
    public int Capacity { get; set; }

    /// <summary>
    /// The name of the rental unit
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Name must exist.")]
    [MaxLength(100, ErrorMessage = "Name must be fewer than 100 characters")]
    public string Name { get; set; }

    /// <summary>
    /// Id of the rental
    /// </summary>
    /// <value></value>
    public int RentalId { get; set; }

    /// <summary>
    /// The rental model associated with the rental unit
    /// </summary>
    /// <value></value>
    public RentalModel Rental { get; set; }

    /// <summary>
    /// the size of the rental unit
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Size must exist")]
    [RegularExpression(@"^\d+x\d+$", ErrorMessage = "Size must be in the form '10x10'")]
    public string Size { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
