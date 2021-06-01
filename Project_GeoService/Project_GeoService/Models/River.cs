using Project_GeoService.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_GeoService.Models
{
    public class River : BaseEntity
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "{0} must be greater than {1}.")]
        public double Length { get; set; }

        [Required]
        public virtual ICollection<CountryRiver> CountryRivers { get; set; }
    }
}