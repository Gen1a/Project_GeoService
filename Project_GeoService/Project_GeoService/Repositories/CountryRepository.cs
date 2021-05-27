using Project_GeoService.Exceptions;
using Project_GeoService.Interfaces;
using Project_GeoService.Models;
using System.Collections.Generic;

namespace Project_GeoService.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private Dictionary<int, Country> data = new Dictionary<int, Country>();

        public CountryRepository()
        {
            data.Add(1, new Country(1, "België", "Brussel", "Europa"));
            data.Add(2, new Country(2, "Peru", "Lima", "Zuid-Amerika"));
            data.Add(3, new Country(3, "Duitsland", "Berlijn", "Europa"));
            data.Add(4, new Country(4, "Zweden", "Stockholm", "Europa"));
            data.Add(5, new Country(5, "Noorwegen", "Oslo", "Europa"));
        }

        public void AddCountry(Country country)
        {
            if (!data.ContainsKey(country.Id))
            {
                data.Add(country.Id, country);
            }
            else throw new CountryException($"Country with Id {country.Id} is already present.");
        }

        public IEnumerable<Country> GetAll()
        {
            if (!(data.Count == 0))
            {
                return data.Values;
            }
            else throw new CountryException($"No Countries present in the database.");
        }

        public Country GetCountry(int id)
        {
            if (data.ContainsKey(id))
            {
                return data[id];
            }
            else throw new CountryException($"Country with Id {id} doesn't exist.");
        }

        public void RemoveCountry(Country country)
        {
            if (data.ContainsKey(country.Id))
            {
                data.Remove(country.Id);
            }
            else throw new CountryException($"Country with Id {country.Id} doesn't exist.");
        }

        public void UpdateCountry(Country country)
        {
            if (data.ContainsKey(country.Id))
            {
                data[country.Id] = country;
            }
            else throw new CountryException($"Country with Id {country.Id} doesn't exist.");
        }
    }
}
