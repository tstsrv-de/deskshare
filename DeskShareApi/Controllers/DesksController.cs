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
    public class DesksController : ControllerBase
    {
        private readonly DbContextDeskShare _context;
        private readonly ILogger<UserController> _logger;
        public DesksController(DbContextDeskShare context, ILogger<UserController> logger)
        {
            _context = context; _logger = logger;
        }

        #region Logging

        private void LogInformation(string message)
        {
            _logger.LogInformation($"{DateTime.Now} - Information:{Environment.NewLine}{message}");
        }
        private void LogWarning(string message)
        {
            _logger.LogWarning($"{DateTime.Now} - Warning:{Environment.NewLine}{message}");
        }
        private void LogError(string message)
        {
            _logger.LogError($"{DateTime.Now} - Error:{Environment.NewLine}{message}");
        }

        #endregion


        // GET: api/Desks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Desks>>> Get_Desks(bool mouse, bool keyboard, bool computer, bool docking, bool noscreen, bool onescreen, bool twoscreens, bool threescreens)
        {
             

            if (!mouse&&!keyboard&&!computer&&!docking&&!noscreen&&!onescreen&&!twoscreens&&!threescreens)
            {
                LogInformation("get all desks without filter");
                return await _context._Desks.OrderBy(x => x._Order).ToListAsync();
            }

            LogInformation($"get all desks with filter: mouse:{mouse}, keyboard:{keyboard}, computer:{computer}, docking:{docking}, noscreen:{noscreen}, onescreen:{onescreen}, twoscreens:{twoscreens}, threescreens:{threescreens}");

            var desks = _context._Desks.AsQueryable();
            if (mouse)
            {
                desks =  desks.Where(x => x._Mouse.Equals(mouse));
            }
            if (keyboard)
            {
                desks = desks.Where(x => x._Keyboard.Equals(keyboard));
            }
            if (computer)
            {
                desks = desks.Where(x => x._Computer.Equals(computer));
            }
            if (docking)
            {
                desks = desks.Where(x => x._Docking.Equals(docking));
            }
            if (onescreen)
            {
                desks = desks.Where(x => x._Screens.Equals(1));
            }
            if (twoscreens)
            {
                desks = desks.Where(x => x._Screens.Equals(2));
            }
            if (threescreens)
            {
                desks = desks.Where(x => x._Screens>=3);
            }
          
            return await desks.ToListAsync();
        }

        // GET: api/Desks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desks>> GetDesks(int id)
        {
            LogInformation($"get desk '{id}'");
            var desks = await _context._Desks.FindAsync(id);

            if (desks == null)
            {
                LogWarning($"desk '{id}' not found");
                return NotFound();
            }

            return desks;
        }

        [HttpGet]
        [Route("byRoom")]
        public async Task<ActionResult<Desks>> GetDesksByRoom(int id)
        {
            LogInformation($"get desks by room '{id}'");
            var desks = await _context._Desks.Where(x=>x._RoomId.Equals(id)).ToListAsync();

            if (desks == null)
            {
                LogWarning($"desks by room '{id}' not found");
                return NotFound();
            }

            return Ok(desks);
        }

        // PUT: api/Desks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesks( Desks desks)
        {
            LogInformation($"edit desk '{desks._Id}'");
           

            _context.Entry(desks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                LogInformation($"desk '{desks._Id}' edited and saved");
            }
            catch (DbUpdateConcurrencyException e)
            {
                LogError($"error by editing desk '{desks._Id}': {e.Message}");
                if (!DesksExists(desks._Id))
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

        // POST: api/Desks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Desks>> PostDesks(Desks desks)
        {
            LogInformation($"create desk '{desks._Name}'");
            _context._Desks.Add(desks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDesks", new { id = desks._Id }, desks);
        }

        // DELETE: api/Desks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesks(int id)
        {
            LogInformation($"delete desk '{id}'");
            var desks = await _context._Desks.FindAsync(id);
            if (desks == null)
            {
                LogWarning($"desk '{id}' not found");
                return NotFound();
            }

            _context._Desks.Remove(desks);
            await _context.SaveChangesAsync();
            LogInformation($"deleted desk '{id}'");
            return NoContent();
        }

        private bool DesksExists(int id)
        {
            return _context._Desks.Any(e => e._Id == id);
        }
    }
}
