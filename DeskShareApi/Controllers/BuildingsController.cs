using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeskShareApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;

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

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> Get_Buildings()
        {
            LogInformation("Get all buildings");
            var buildings= await _context._Buildings.OrderBy(x => x._Order).ToListAsync();
            LogInformation($"{buildings.Count()} buildings found");
            return buildings;
        }
            
        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buildings>> GetBuildings(int id)
        {
            LogInformation($"Get building '{id}'");
            var buildings = await _context._Buildings.FindAsync(id);

            if (buildings == null)
            {
                LogWarning($"building '{id}' not found");
                return NotFound();
            }

            return buildings;
        }

        // PUT: api/Buildings
        public async Task<IActionResult> PutBuildings(Buildings buildings)
        {
          
            LogInformation($"edit building '{buildings._Id}'");
           
            _context.Entry(buildings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                LogInformation($"building '{buildings._Id}' changed");
            }
            catch (DbUpdateConcurrencyException e)
            {
                LogWarning($"DbUpdateConcurrencyException {e.Message}");
                if (!BuildingsExists(buildings._Id))
                {
                    LogError($"{buildings._Id} not found");
                    return NotFound();
                }
                else
                {}
            }

            return NoContent();
        }

        // POST: api/Buildings
        [HttpPost]
        public async Task<ActionResult<Buildings>> PostBuildings(Buildings buildings)
        {
            LogInformation($"create building '{buildings._Name}'");
            if (!TryValidateModel(buildings))
            {
                LogError("'{buildings._Name}' hat die Validierung nicht bestanden");
                return ValidationProblem();
            }

            _context._Buildings.Add(buildings);
            await _context.SaveChangesAsync();
            LogInformation($"saved new building '{buildings._Name}'");
            return CreatedAtAction("GetBuildings", new { id = buildings._Id }, buildings);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuildings(int id)
        {
            LogInformation($"delete building '{id}'");
            var buildings = await _context._Buildings.FindAsync(id);
            if (buildings == null)
            {
                LogWarning($"building '{id}' not found");
                return NotFound();
            }

            _context._Buildings.Remove(buildings);
            await _context.SaveChangesAsync();
            LogInformation($"deleted building '{id}'");
            return NoContent();
        }

        private bool BuildingsExists(int id)
        {
            return _context._Buildings.Any(e => e._Id == id);
        }
    }
}
