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

        // GET: api/continent/1/country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries([FromRoute] long continentId)
        {
            return await _context.Countries.Where(_ => _.ContinentId == continentId).ToListAsync();
        }

        // GET: api/continent/1/country/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Country>> GetCountry(long id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null) return NotFound();

            return country;
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
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
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

        private bool CountryExists(long id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
