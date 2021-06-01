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
    public class RiverControllerEF : ControllerBase
    {
        private readonly Project_GeoServiceContext _context;

        public RiverControllerEF(Project_GeoServiceContext context)
        {
            _context = context;
        }

        // GET: api/RiverControllerEF
        [HttpGet]
        public async Task<ActionResult<IEnumerable<River>>> GetRivers()
        {
            return await _context.Rivers.ToListAsync();
        }

        // GET: api/RiverControllerEF/5
        [HttpGet("{id}")]
        public async Task<ActionResult<River>> GetRiver(long id)
        {
            var river = await _context.Rivers.FindAsync(id);

            if (river == null)
            {
                return NotFound();
            }

            return river;
        }

        // PUT: api/RiverControllerEF/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRiver(long id, River river)
        {
            if (id != river.Id)
            {
                return BadRequest();
            }

            _context.Entry(river).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiverExists(id))
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

        // POST: api/RiverControllerEF
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<River>> PostRiver(River river)
        {
            _context.Rivers.Add(river);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRiver", new { id = river.Id }, river);
        }

        // DELETE: api/RiverControllerEF/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRiver(long id)
        {
            var river = await _context.Rivers.FindAsync(id);
            if (river == null)
            {
                return NotFound();
            }

            _context.Rivers.Remove(river);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RiverExists(long id)
        {
            return _context.Rivers.Any(e => e.Id == id);
        }
    }
}
