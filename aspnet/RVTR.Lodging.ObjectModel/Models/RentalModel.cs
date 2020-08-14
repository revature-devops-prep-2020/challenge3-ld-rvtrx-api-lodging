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

        public string Name { get; set; }

        public int? LodgingId { get; set; }
        public LodgingModel Lodging { get; set; }

        public int Occupancy { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public double Price { get; set; }

        public double? DiscountedPrice { get; set; }

        /// <summary>
        /// Represents the _Rental_ `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
