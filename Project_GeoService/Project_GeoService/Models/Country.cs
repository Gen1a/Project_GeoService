namespace Project_GeoService.Models
{
    public class Country
    {
        public Country(string name, string capital, string continent)
        {
            Name = name;
            Capital = capital;
            Continent = continent;
        }

        public Country(int id, string name, string capital, string continent) : this(name, capital, continent) => Id = id;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Continent { get; set; }
    }
}
