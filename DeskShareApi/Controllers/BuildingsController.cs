using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeskShareApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace DeskShareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuildingsController : ControllerBase
    {
        private readonly DbContextDeskShare _context;
        private readonly ILogger<BuildingsController> _logger;

        public BuildingsController(DbContextDeskShare context, ILogger<BuildingsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> Get_Buildings()
        {
            _logger.LogInformation("Get_Buildings:");
            var buildings= await _context._Buildings.OrderBy(x => x._Order).ToListAsync();
            _logger.LogInformation($"{buildings.Count()} rows found");
            return buildings;
        }
            
        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buildings>> GetBuildings(int id)
        {
            var buildings = await _context._Buildings.FindAsync(id);

            if (buildings == null)
            {
                return NotFound();
            }

            return buildings;
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildings(int id, Buildings buildings)
        {
            if (id != buildings._Id)
            {
                return BadRequest();
            }

            _context.Entry(buildings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingsExists(id))
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

        // POST: api/Buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Buildings>> PostBuildings(Buildings buildings)
        {
            _context._Buildings.Add(buildings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildings", new { id = buildings._Id }, buildings);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuildings(int id)
        {
            var buildings = await _context._Buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }

            _context._Buildings.Remove(buildings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingsExists(int id)
        {
            return _context._Buildings.Any(e => e._Id == id);
        }
    }
}
