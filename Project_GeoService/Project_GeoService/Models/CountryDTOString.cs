using Project_GeoService.Interfaces;
using System.Collections.Generic;

namespace Project_GeoService.Models
{
    public class CountryDTOString : BaseEntity
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public double Surface { get; set; }
        public IEnumerable<string> CountryCapitals { get; set; }
        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<string> CountryRivers { get; set; }
        public long? ContinentId { get; set; }
    }
}