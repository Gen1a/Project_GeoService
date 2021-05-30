using Project_GeoService.Exceptions;
using Project_GeoService.Interfaces;
using Project_GeoService.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project_GeoService.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private Dictionary<long, Country> data = new Dictionary<long, Country>();

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

        public IEnumerable<Country> GetAll(string continent, string capital)
        {
            if (!(data.Count == 0) && !string.IsNullOrEmpty(continent) && !string.IsNullOrEmpty(capital))
            {
                return data.Values.Where(_ => _.Continent.ToLower() == continent.ToLower() && _.Capital.ToLower() == capital.ToLower());
            }
            else throw new CountryException($"No Countries present with continent '{continent}' and capital '{capital}' in the database.");
        }

        public IEnumerable<Country> GetAllByContinent(string continent)
        {
            if (!(data.Count == 0) && !string.IsNullOrEmpty(continent))
            {
                return data.Values.Where(_ => _.Continent.ToLower() == continent.ToLower());
            }
            else throw new CountryException($"No Countries present with continent '{continent}' in the database.");
        }

        public IEnumerable<Country> GetAllByCapital(string capital)
        {
            if (!(data.Count == 0) && !string.IsNullOrEmpty(capital))
            {
                return data.Values.Where(_ => _.Capital.ToLower() == capital.ToLower());
            }
            else throw new CountryException($"No Countries present with capital '{capital}' in the database.");
        }


        public Country GetCountry(long id)
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