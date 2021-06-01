using Project_GeoService.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_GeoService.Models
{
    public class Country : BaseEntity
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        public int Population { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        public double Surface { get; set; }

        public virtual ICollection<CountryCapital> CountryCapitals { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<CountryRiver> CountryRivers { get; set; }

        [Required]
        public Continent Continent { get; set; }

        [Range(0, long.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        public long ContinentId { get; set; }
    }
}
