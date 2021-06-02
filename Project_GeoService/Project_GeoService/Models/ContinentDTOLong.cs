using Project_GeoService.Interfaces;
using System.Collections.Generic;

namespace Project_GeoService.Models
{
    public class ContinentDTOLong : BaseEntity
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public virtual IEnumerable<long> Countries { get; set; }
    }
}