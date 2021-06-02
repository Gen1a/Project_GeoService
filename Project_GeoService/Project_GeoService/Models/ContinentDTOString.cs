using Project_GeoService.Interfaces;
using System.Collections.Generic;

namespace Project_GeoService.Models
{
    public class ContinentDTOString : BaseEntity
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public virtual IEnumerable<string> Countries { get; set; }
    }
}