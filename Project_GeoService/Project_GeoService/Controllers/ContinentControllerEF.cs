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
    [Route("api/continent")]
    [ApiController]
    public class ContinentControllerEF : ControllerBase
    {
        private readonly Project_GeoServiceContext _context;

        public ContinentControllerEF(Project_GeoServiceContext context)
        {
            _context = context;
        }

        #region Routes
        // GET: api/continent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContinentDTOString>>> GetContinents()
        {
            var continents = await _context.Continents.ToListAsync();
            List<ContinentDTOString> dtos = new List<ContinentDTOString>();
            if (continents.Any())
            {
                foreach (Continent c in continents)
                {
                    dtos.Add(ContinentToDTOString(c));
                }
            }

            return dtos;
        }

        // GET: api/continent/5
        [HttpGet("{id:long}", Name = "GetContinent")]
        public async Task<ActionResult<ContinentDTOString>> GetContinent(long id)
        {
            var continent = await _context.Continents.FindAsync(id);
            if (continent == null) return NotFound();

            ContinentDTOString dto = ContinentToDTOString(continent);
            return dto;
        }

        // PUT: api/continent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutContinent(long id, [FromBody] ContinentDTOLong continentDTO)
        {
            if (id != continentDTO.Id) return BadRequest("Query parameter 'Id' has to be identical to the Id of the Continent in the body.");

            Continent continent = await _context.Continents.FindAsync(id);
            if (continent == null) return NotFound($"Continent with Id '{id}' doesn't exist.");

            foreach (var countryId in continentDTO.Countries)
            {
                Country country = await _context.Countries.FindAsync(countryId);
                if (country == null) return NotFound($"The specified Country with Id '{countryId}' doesn't exist.");

                country.ContinentId = continent.Id;
                _context.Entry(country).State = EntityState.Modified;
            }
            _context.Entry(continent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(id))
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

        // POST: api/continent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Continent>> PostContinent([FromBody] ContinentDTOLong continentDTO)
        {
            if (ContinentExists(continentDTO.Name)) return BadRequest($"Continent with Name '{continentDTO.Name}' already exists.");

            Continent toPost = new Continent { Name = continentDTO.Name };
            if (continentDTO.Countries != null && continentDTO.Countries.Any())
            {
                toPost.Countries = new List<Country>();

                foreach (var countryId in continentDTO.Countries)
                {
                    Country country = await _context.Countries.FindAsync(countryId);
                    if (country == null) return NotFound($"The specified Country with Id '{countryId}' doesn't exist.");

                    toPost.Countries.Add(country);
                    toPost.Population += country.Population;
                    _context.Entry(country).State = EntityState.Modified;
                }
            }

            _context.Continents.Add(toPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContinent), new { id = toPost.Id }, ContinentToDTOString(toPost));
        }

        // DELETE: api/continent/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteContinent(long id)
        {
            var continent = await _context.Continents.FindAsync(id);
            if (continent == null) return NotFound();
            if (_context.Countries.Any(_ => _.ContinentId == id)) return BadRequest("Cannot delete a Continent when there are still Countries with this Continent in the database.");

            _context.Continents.Remove(continent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region HELPERS
        private ContinentDTOString ContinentToDTOString(Continent continent) =>
            new ContinentDTOString
            {
                Id = continent.Id,
                Name = continent.Name,
                Population = GetPopulation(continent.Id),
                Countries = GetCountriesAsURL(continent.Id).Result
            };

        private bool ContinentExists(long id)
        {
            return _context.Continents.Any(e => e.Id == id);
        }

        private bool ContinentExists(string name)
        {
            return _context.Continents.Any(e => e.Name == name);
        }

        private int GetPopulation(long continentId) => _context.Countries.Where(_ => _.ContinentId == continentId).Sum(_ => _.Population);

        private async Task<IEnumerable<string>> GetCountriesAsURL(long continentId)
        {
            List<string> countryURLs = new List<string>();
            List<Country> countries = await _context.Countries.Where(_ => _.ContinentId == continentId).ToListAsync();
            foreach (Country country in countries)
            {
                countryURLs.Add($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{continentId}/{typeof(Country).Name}/{country.Id}");
            }
            return countryURLs;
        }
        #endregion
    }
}
