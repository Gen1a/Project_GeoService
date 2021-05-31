using Project_GeoService.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Project_GeoService.Models
{
    public class Continent : BaseEntity
    {
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        public int Population => Countries.Sum(_ => (int?)_.Population) ?? 0;

        public virtual ICollection<Country> Countries { get; set; }
    }
}
