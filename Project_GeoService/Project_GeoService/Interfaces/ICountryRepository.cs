using Project_GeoService.Models;
using System.Collections.Generic;

namespace Project_GeoService.Interfaces
{
    public interface ICountryRepository
    {
        void AddCountry(Country country);

        Country GetCountry(long id);

        IEnumerable<Country> GetAll();

        IEnumerable<Country> GetAll(string continent, string capital);

        IEnumerable<Country> GetAllByContinent(string continent);

        IEnumerable<Country> GetAllByCapital(string capital);

        void RemoveCountry(Country country);

        void UpdateCountry(Country country);
    }
}
