using Project_GeoService.Interfaces;
using System.Collections.Generic;

namespace Project_GeoService.Models
{
    public class CountryDTOLong : BaseEntity
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public double Surface { get; set; }
        public IEnumerable<long> CountryCapitals { get; set; }
        public IEnumerable<long> Cities { get; set; }
        public IEnumerable<long> CountryRivers { get; set; }
        public long? ContinentId { get; set; }
    }
}