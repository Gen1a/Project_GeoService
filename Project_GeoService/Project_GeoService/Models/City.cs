using Project_GeoService.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Project_GeoService.Models
{
    public class City : BaseEntity
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        public int Population { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        public double Surface { get; set; }

        [Required]
        public Country Country { get; set; }

        [Range(0, long.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        public long CountryId { get; set; }
    }
}