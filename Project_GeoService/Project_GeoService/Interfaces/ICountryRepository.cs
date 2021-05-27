using Project_GeoService.Models;
using System.Collections.Generic;

namespace Project_GeoService.Interfaces
{
    public interface ICountryRepository
    {
        void AddCountry(Country country);

        Country GetCountry(int id);

        IEnumerable<Country> GetAll();

        void RemoveCountry(Country country);

        void UpdateCountry(Country country);
    }
}
