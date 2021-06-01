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
    [Route("api/[controller]")]
    [ApiController]
    public class CountryCapitalControllerEF : ControllerBase
    {
        private readonly Project_GeoServiceContext _context;

        public CountryCapitalControllerEF(Project_GeoServiceContext context)
        {
            _context = context;
        }

        // GET: api/CountryCapitalControllerEF
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryCapital>>> GetCountryCapitals()
        {
            return await _context.CountryCapitals.ToListAsync();
        }

        // GET: api/CountryCapitalControllerEF/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryCapital>> GetCountryCapital(long id)
        {
            var countryCapital = await _context.CountryCapitals.FindAsync(id);

            if (countryCapital == null)
            {
                return NotFound();
            }

            return countryCapital;
        }

        // PUT: api/CountryCapitalControllerEF/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryCapital(long id, CountryCapital countryCapital)
        {
            if (id != countryCapital.CountryId)
            {
                return BadRequest();
            }

            _context.Entry(countryCapital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryCapitalExists(id))
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

        // POST: api/CountryCapitalControllerEF
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryCapital>> PostCountryCapital(CountryCapital countryCapital)
        {
            _context.CountryCapitals.Add(countryCapital);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CountryCapitalExists(countryCapital.CountryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCountryCapital", new { id = countryCapital.CountryId }, countryCapital);
        }

        // DELETE: api/CountryCapitalControllerEF/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryCapital(long id)
        {
            var countryCapital = await _context.CountryCapitals.FindAsync(id);
            if (countryCapital == null)
            {
                return NotFound();
            }

            _context.CountryCapitals.Remove(countryCapital);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryCapitalExists(long id)
        {
            return _context.CountryCapitals.Any(e => e.CountryId == id);
        }
    }
}
