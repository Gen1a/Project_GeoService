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

        // GET: api/continent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Continent>>> GetContinents()
        {
            var continents = await _context.Continents.ToListAsync();
            foreach (Continent c in continents)
            {
                var countries = await _context.Countries.Where(_ => _.ContinentId == c.Id).ToListAsync();
                foreach (Country country in countries)
                {
                    c.Countries.Add(country);
                    c.Population += country.Population;
                }
            }
            return continents;
        }

        // GET: api/continent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContinentDTO>> GetContinent(long id)
        {
            var continent = await _context.Continents.FindAsync(id);
            if (continent == null) return NotFound();

            //continent.Population = GetPopulation(id);
            var foo = ContinentToDTO(continent);
            return foo;
        }

        // PUT: api/continent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContinent(long id, Continent continent)
        {
            if (id != continent.Id)
            {
                return BadRequest();
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
        public async Task<ActionResult<Continent>> PostContinent([FromBody] Continent continent)
        {
            if (ContinentExists(continent.Name)) return BadRequest();

            _context.Continents.Add(continent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContinent", new { id = continent.Id }, continent);
        }

        // DELETE: api/continent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContinent(long id)
        {
            var continent = await _context.Continents.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            _context.Continents.Remove(continent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private ContinentDTO ContinentToDTO(Continent continent) =>
            new ContinentDTO
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
                countryURLs.Add($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{typeof(Country).Name}/{country.Id}");
            }
            return countryURLs;
        }
    }
}
