using System.ComponentModel.DataAnnotations;

namespace Project_GeoService.Models
{
    public class Country
    {
        public Country() { }

        public Country(string name, string capital, string continent)
        {
            Name = name;
            Capital = capital;
            Continent = continent;
        }

        public Country(long id, string name, string capital, string continent) : this(name, capital, continent) => Id = id;

        [Range(1, long.MaxValue, ErrorMessage ="{0} must be greater than {1}.")]
        public long Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Capital { get; set; }

        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        public string Continent { get; set; }
    }
}
