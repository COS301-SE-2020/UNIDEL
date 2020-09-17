using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniDelAPI.Models;

namespace UniDelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallLogController : ControllerBase
    {
        private readonly UniDelDbContext _context;

        public CallLogController(UniDelDbContext context)
        {
            _context = context;
        }

        // GET: api/CallLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallLog>>> GetLog([FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            return await _context.CallLogs.ToListAsync();
        }

        // GET: api/CallLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CallLog>> GetLog(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var log = await _context.CallLogs.FindAsync(id);

            if (log == null)
            {
                return NotFound();
            }

            return log;
        }

        // PUT: api/CallLog/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLog(int id, CallLog log, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            if (id != log.CallLogID)
            {
                return BadRequest();
            }

            _context.Entry(log).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id))
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

        // POST: api/CallLog
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CallLog>> PostLog(CallLog log, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            _context.CallLogs.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLog", new { id = log.CallLogID }, log);
        }

        // DELETE: api/CallLog/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CallLog>> DeleteLog(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var log = await _context.CallLogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            _context.CallLogs.Remove(log);
            await _context.SaveChangesAsync();

            return log;
        }

        private bool LogExists(int id)
        {
            return _context.CallLogs.Any(e => e.CallLogID == id);
        }
    }
}
