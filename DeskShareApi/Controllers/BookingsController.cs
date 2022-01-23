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
using Newtonsoft.Json;

namespace DeskShareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly DbContextDeskShare _context;
        private readonly ILogger<UserController> _logger;
        public BookingsController(DbContextDeskShare context, ILogger<UserController> logger)
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

        //GET: CheckBlock
        [HttpGet]
        [Route("CheckBooking")]
        public  ActionResult<bool> CheckBooking(DateTime start, DateTime end,int desk)
        {
            LogInformation($"check booking between '{start}' & '{end}'");
            var rtn =  _context._Bookings.Where(x=>x._Desk.Equals(desk)&&((start <= x._Start && end <= x._End && end >= x._Start) ||
            (start >= x._Start && start <= x._End && end >= x._End) ||
            (start >= x._Start && end <= x._End) ||
            (start <= x._Start && end >= x._End))); 
            if (rtn.Any())
            {
                LogInformation("Booking span blocked!");
                return Ok(false);
            }
            LogInformation("Booking span free");
            return Ok(true);
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookings>>> Get_Bookings(bool freetoday,bool freetomorrow,bool freeweek)
        {
            if (!freetoday&&!freetomorrow&&!freeweek)
            {
                return await _context._Bookings.Where(x=>x._End>DateTime.Now).OrderBy(x => x._End).ToListAsync();
            }

            //Gegenteil auswählen um eine Ebene tiefer Desks auszusortieren
            if (freetoday)
            {
                var dtn = DateTime.Now;
                var start = new DateTime(dtn.Year, dtn.Month, dtn.Day, 0, 0, 0);
                var end = new DateTime(dtn.Year, dtn.Month, dtn.Day, 23, 59, 59);

                var rtn= await _context._Bookings.Where(x => (start <= x._Start && end <= x._End && end >= x._Start) ||
            (start >= x._Start && start <= x._End && end >= x._End) ||
            (start >= x._Start && end <= x._End) ||
            (start <= x._Start && end >= x._End)).ToListAsync();
                return rtn;

            }
            if (freetomorrow)
            {
                var dtn2 = DateTime.Now;
                var start2 = new DateTime(dtn2.Year, dtn2.Month, dtn2.Day+1, 0, 0, 0);
                var end2 = new DateTime(dtn2.Year, dtn2.Month, dtn2.Day+1, 23, 59, 59);

                var rtn2 = await _context._Bookings.Where(x => (start2 <= x._Start && end2 <= x._End && end2 >= x._Start) ||
             (start2 >= x._Start && start2 <= x._End && end2 >= x._End) ||
             (start2 >= x._Start && end2 <= x._End) ||
             (start2 <= x._Start && end2 >= x._End)).ToListAsync();
                return rtn2;
            }
            var dtn3 = DateTime.Now;
            var start3 = new DateTime(dtn3.Year, dtn3.Month, dtn3.Day, 0, 0, 0);
            var dow3 = (int)DateTime.Now.DayOfWeek;
            var end3 = new DateTime(dtn3.Year, dtn3.Month,dtn3.Day+(7-dow3), 23, 59, 59);

            var rtn3 = await _context._Bookings.Where(x => (start3 <= x._Start && end3 <= x._End && end3 >= x._Start) ||
         (start3 >= x._Start && start3 <= x._End && end3 >= x._End) ||
         (start3 >= x._Start && end3 <= x._End) ||
         (start3 <= x._Start && end3 >= x._End)).ToListAsync();
            return rtn3;
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookings>> GetBookings(int id)
        {
            var bookings = await _context._Bookings.FindAsync(id);

            if (bookings == null)
            {
                return NotFound();
            }

            return bookings;
        }

        // GET: api/Bookings/byUser?id=xxxxxx&status=true
        [HttpGet]
        [Route("byUser")]
        public async Task<ActionResult<IEnumerable<Bookings>>> GetBookingsByUser(string id,bool all)
        {
            if (all)
            {
                var bookingsAll = await _context._Bookings.Where(x => x._User.Equals(id)).ToListAsync();

                if (bookingsAll == null)
                {
                    return NotFound();
                }

                return Ok(bookingsAll);
            }
                var bookings = await _context._Bookings.Where(x => x._User.Equals(id) && x._End > DateTime.Now).ToListAsync() ;
            
            

            if (bookings == null)
            {
                return NotFound();
            }

            return Ok(bookings);
        }

        
        // GET: api/Bookings/byDesk?id=xxxxxx&status=true
        [HttpGet]
        [Route("byDesk")]
        public async Task<ActionResult<IEnumerable<Bookings>>> GetBookingsByDesk(int id,bool? status)
        {
           
           
                var bookings = await _context._Bookings.Where(x => x._Desk.Equals(id) && x._End > DateTime.Now).ToListAsync();
          
         

            if (bookings == null)
            {
                return NotFound();
            }

            return Ok(bookings);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookings(int id, Bookings bookings)
        {
            if (id != bookings._Id)
            {
                return BadRequest();
            }

            _context.Entry(bookings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingsExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookings>> PostBookings(Bookings bookings)
        {
            LogInformation($"Post booking on desk {bookings._Desk} from {bookings._User} from {bookings._Start} till {bookings._End}");

            _context._Bookings.Add(bookings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookings", new { id = bookings._Id }, bookings);
        }

        // DELETE: api/Bookings/5?uid=""
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookings(int id,string uid)
        {
            var bookings = await _context._Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }

            if (bookings._User!=uid)
            {
                return Unauthorized();
            }
            _context._Bookings.Remove(bookings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingsExists(int id)
        {
            return _context._Bookings.Any(e => e._Id == id);
        }
    }
}
