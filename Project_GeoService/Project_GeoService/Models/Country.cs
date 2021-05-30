﻿namespace Project_GeoService.Models
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

        public long Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Continent { get; set; }
    }
}
