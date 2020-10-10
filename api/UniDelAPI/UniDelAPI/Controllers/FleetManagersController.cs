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
    public class FleetManagersController : ControllerBase
    {
        private readonly UniDelDbContext _context;

        public FleetManagersController(UniDelDbContext context)
        {
            _context = context;
        }

        // GET: api/FleetManagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FleetManager>>> GetFleetManagers([FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            return await _context.FleetManagers.ToListAsync();
        }

        // GET: api/FleetManagers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FleetManager>> GetFleetManager(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var fleetManager = await _context.FleetManagers.FindAsync(id);

            if (fleetManager == null)
            {
                return NotFound();
            }

            return fleetManager;
        }

        // PUT: api/FleetManagers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFleetManager(int id, FleetManager fleetManager, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            if (id != fleetManager.FleetManagerID)
            {
                return BadRequest();
            }

            _context.Entry(fleetManager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FleetManagerExists(id))
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

        // POST: api/FleetManagers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FleetManager>> PostFleetManager(FleetManager fleetManager, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            _context.FleetManagers.Add(fleetManager);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFleetManager", new { id = fleetManager.FleetManagerID }, fleetManager);
        }

        // DELETE: api/FleetManagers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FleetManager>> DeleteFleetManager(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var fleetManager = await _context.FleetManagers.FindAsync(id);
            if (fleetManager == null)
            {
                return NotFound();
            }

            _context.FleetManagers.Remove(fleetManager);
            await _context.SaveChangesAsync();

            return fleetManager;
        }

        private bool FleetManagerExists(int id)
        {
            return _context.FleetManagers.Any(e => e.FleetManagerID == id);
        }
    }
}
