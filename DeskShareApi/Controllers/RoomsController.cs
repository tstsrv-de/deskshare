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
    public class RoomsController : ControllerBase
    {
        private readonly DbContextDeskShare _context;
        private readonly ILogger<UserController> _logger;

        public RoomsController(DbContextDeskShare context, ILogger<UserController> logger)
        {
            _context = context; _logger = logger;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rooms>>> Get_Rooms()
        {
            return await _context._Rooms.OrderBy(x => x._Order).ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rooms>> GetRooms(int id)
        {
            var rooms = await _context._Rooms.FindAsync(id);

            if (rooms == null)
            {
                return NotFound();
            }

            return rooms;
        }

        // GET: api/Rooms/byFloors?id=5
        [HttpGet]
        [Route("byFloor")]
        public async Task<ActionResult<IEnumerable<Rooms>>> GetRoomsByFloor(int id)
        {
            return Ok(await _context._Rooms.Where(x => x._FloorId.Equals(id)).OrderBy(x => x._Order).ToListAsync());
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRooms( Rooms rooms)
        {
         
         

            _context.Entry(rooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomsExists(rooms._Id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rooms>> PostRooms(Rooms rooms)
        {
            _context._Rooms.Add(rooms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRooms", new { id = rooms._Id }, rooms);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRooms(int id)
        {
            var rooms = await _context._Rooms.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }

            _context._Rooms.Remove(rooms);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomsExists(int id)
        {
            return _context._Rooms.Any(e => e._Id == id);
        }
    }
}
