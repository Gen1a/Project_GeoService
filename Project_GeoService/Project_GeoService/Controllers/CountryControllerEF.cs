using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_GeoService.Data;
using Project_GeoService.Models;

namespace Project_GeoService.Controllers
{
    [Route("api/continent/{continentId:long}/country")]
    [ApiController]
    public class CountryControllerEF : ControllerBase
    {
        private readonly Project_GeoServiceContext _context;

        public CountryControllerEF(Project_GeoServiceContext context)
        {
            _context = context;
        }

        #region ROUTES
        // GET: api/continent/1/country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries([FromRoute] long continentId)
        {
            return await _context.Countries.Where(_ => _.ContinentId == continentId).ToListAsync();
        }

        // GET: api/continent/1/country/5
        [HttpGet("{id:long}")]
        [ActionName("GetCountryAsync")]
        public async Task<ActionResult<CountryDTOString>> GetCountryAsync(long id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null) return NotFound();

            return CountryToDTOString(country);
        }

        // PUT: api/continent/1/country/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(long id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/continent/1/country
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry([FromBody] CountryDTOLong countryDTO)
        {
            if (countryDTO.ContinentId == null) return BadRequest("A Country needs to be assigned to a Continent.");
            if (ExistsInContinent(countryDTO.Name, (long)countryDTO.ContinentId)) return BadRequest("A Country's Name needs to be unique within a Continent.");
            
            Country toPost = new Country
            {
                Name = countryDTO.Name,
                Population = countryDTO.Population,
                Surface = countryDTO.Surface,
                ContinentId = (long)countryDTO.ContinentId
            };
            _context.Countries.Add(toPost);
            await _context.SaveChangesAsync();

            // Handle Country Capitals
            if (countryDTO.CountryCapitals != null && countryDTO.CountryCapitals.Any())
            {
                toPost.CountryCapitals = new List<CountryCapital>();
                _context.Entry(toPost).State = EntityState.Modified;
                foreach (var cityId in countryDTO.CountryCapitals)
                {
                    City city = await _context.Cities.FindAsync(cityId);
                    if (city == null) return NotFound($"The specified Capital City with Id '{cityId}' doesn't exist.");

                    toPost.CountryCapitals.Add(new CountryCapital() { CountryId = toPost.Id, CityId = cityId });
                }
            }

            // Handle Country Cities
            if (countryDTO.Cities != null && countryDTO.Cities.Any())
            {
                toPost.Cities = new List<City>();
                foreach (var cityId in countryDTO.Cities)
                {
                    City city = await _context.Cities.FindAsync(cityId);
                    if (city == null) return NotFound($"The specified City with Id '{cityId}' doesn't exist.");

                    toPost.Cities.Add(city);
                    _context.Entry(city).State = EntityState.Modified;
                }
            }

            // Handle Country Rivers
            if (countryDTO.CountryRivers != null && countryDTO.CountryRivers.Any())
            {
                toPost.CountryRivers = new List<CountryRiver>();
                _context.Entry(toPost).State = EntityState.Modified;
                foreach (var riverId in countryDTO.CountryRivers)
                {
                    River river = await _context.Rivers.FindAsync(riverId);
                    if (river == null) return NotFound($"The specified River with Id '{riverId}' doesn't exist.");

                    toPost.CountryRivers.Add(new CountryRiver() { CountryId = toPost.Id, RiverId = river.Id });
                }
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCountryAsync), new { id = toPost.Id }, CountryToDTOString(toPost));
        }

        // DELETE: api/continent/1/country/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(long id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region HELPERS
        private bool CountryExists(long id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        private bool ExistsInContinent(string countryName, long continentId)
        {
            List<Country> countries = _context.Countries.Where(_ => _.ContinentId == continentId).ToList();
            return countries.Any(_ => _.Name == countryName);
        }

        private CountryDTOString CountryToDTOString(Country country) =>
            new CountryDTOString
            {
                Id = country.Id,
                Name = country.Name,
                Population = country.Population,
                Surface = country.Surface,
                CountryCapitals = GetCapitalsAsURL(country.Id, country.ContinentId).Result
            };

        private async Task<IEnumerable<string>> GetCapitalsAsURL(long countryId, long continentId)
        {
            List<string> capitalUrls = new List<string>();
            List<CountryCapital> capitals = await _context.CountryCapitals.Where(_ => _.CountryId == countryId).ToListAsync();
            foreach (CountryCapital capital in capitals)
            {
                capitalUrls.Add($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{continentId}/{typeof(Country).Name}/{countryId}/{typeof(City)}/{capital.CityId}");
            }
            return capitalUrls;
        }
        #endregion
    }
}
