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
    ///
    /// </summary>
    /// <value></value>
    public int RentalId { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public int Capacity { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public RentalModel Rental { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Size { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
