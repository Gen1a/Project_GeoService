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
    public class CountryRiverControllerEF : ControllerBase
    {
        private readonly Project_GeoServiceContext _context;

        public CountryRiverControllerEF(Project_GeoServiceContext context)
        {
            _context = context;
        }

        // GET: api/CountryRiverControllerEF
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryRiver>>> GetCountryRivers()
        {
            return await _context.CountryRivers.ToListAsync();
        }

        // GET: api/CountryRiverControllerEF/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryRiver>> GetCountryRiver(long id)
        {
            var countryRiver = await _context.CountryRivers.FindAsync(id);

            if (countryRiver == null)
            {
                return NotFound();
            }

            return countryRiver;
        }

        // PUT: api/CountryRiverControllerEF/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryRiver(long id, CountryRiver countryRiver)
        {
            if (id != countryRiver.CountryId)
            {
                return BadRequest();
            }

            _context.Entry(countryRiver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryRiverExists(id))
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

        // POST: api/CountryRiverControllerEF
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryRiver>> PostCountryRiver(CountryRiver countryRiver)
        {
            _context.CountryRivers.Add(countryRiver);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CountryRiverExists(countryRiver.CountryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCountryRiver", new { id = countryRiver.CountryId }, countryRiver);
        }

        // DELETE: api/CountryRiverControllerEF/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryRiver(long id)
        {
            var countryRiver = await _context.CountryRivers.FindAsync(id);
            if (countryRiver == null)
            {
                return NotFound();
            }

            _context.CountryRivers.Remove(countryRiver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryRiverExists(long id)
        {
            return _context.CountryRivers.Any(e => e.CountryId == id);
        }
    }
}
