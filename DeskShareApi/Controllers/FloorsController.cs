using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class FloorsController : ControllerBase
    {
        private readonly DbContextDeskShare _context;
        private readonly ILogger<UserController> _logger;
        public FloorsController(DbContextDeskShare context, ILogger<UserController> logger)
        {
            _context = context; _logger = logger;
        }

        // GET: api/Floors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Floors>>> Get_Floors()
        {
            return await _context._Floors.OrderBy(x => x._Order).ToListAsync();
        }

        // GET: api/Floors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Floors>> GetFloors(int id)
        {
            var floors = await _context._Floors.FindAsync(id);

            if (floors == null)
            {
                return NotFound();
            }

            return floors;
        }


        // GET: api/Floors/byBuilding?id=5
        [HttpGet]
        [Route("byBuilding")]
        public async Task<ActionResult<IEnumerable<Floors>>> GetFloorsByBuilding(int id)
        {
          return Ok(await _context._Floors.Where(x => x._BuildingId.Equals(id)).OrderBy(x => x._Order).ToListAsync());       
        }

        // PUT: api/Floors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFloors(Floors floors)
        {

            _context.Entry(floors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorsExists(floors._Id))
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

        // POST: api/Floors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Floors>> PostFloors(Floors floors)
        {
            _context._Floors.Add(floors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFloors", new { id = floors._Id }, floors);
        }

        // DELETE: api/Floors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFloors(int id)
        {
            var floors = await _context._Floors.FindAsync(id);
            if (floors == null)
            {
                return NotFound();
            }

            _context._Floors.Remove(floors);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FloorsExists(int id)
        {
            return _context._Floors.Any(e => e._Id == id);
        }
    }
}
