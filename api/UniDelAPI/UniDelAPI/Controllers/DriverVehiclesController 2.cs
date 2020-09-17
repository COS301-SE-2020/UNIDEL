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
    public class DriverVehiclesController : ControllerBase
    {
        private readonly UniDelDbContext _context;

        public DriverVehiclesController(UniDelDbContext context)
        {
            _context = context;
        }

        // GET: api/DriverVehicles
        [Route("~/api/DriverVehicles/GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverVehicle>>> GetDriverVehicle([FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            return await _context.DriverVehicle.ToListAsync();
        }

        // GET: api/DriverVehicles/5
        [Route("~/api/DriverVehicles/Get/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverVehicle>> GetDriverVehicle(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var driverVehicle = await _context.DriverVehicle.FindAsync(id);

            if (driverVehicle == null)
            {
                return NotFound();
            }

            return driverVehicle;
        }

        // PUT: api/DriverVehicles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Route("~/api/DriverVehicles/Put/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverVehicle(int id, DriverVehicle driverVehicle, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            if (id != driverVehicle.DriverVehicleID)
            {
                return BadRequest();
            }

            _context.Entry(driverVehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverVehicleExists(id))
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

        // POST: api/DriverVehicles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Route("~/api/DriverVehicles/Post")]
        [HttpPost]
        public async Task<ActionResult<DriverVehicle>> PostDriverVehicle(DriverVehicle driverVehicle, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            _context.DriverVehicle.Add(driverVehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverVehicle", new { id = driverVehicle.DriverVehicleID }, driverVehicle);
        }

        // DELETE: api/DriverVehicles/5
        [Route("~/api/DriverVehicles/Delete/{id}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DriverVehicle>> DeleteDriverVehicle(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var driverVehicle = await _context.DriverVehicle.FindAsync(id);
            if (driverVehicle == null)
            {
                return NotFound();
            }

            _context.DriverVehicle.Remove(driverVehicle);
            await _context.SaveChangesAsync();

            return driverVehicle;
        }

        private bool DriverVehicleExists(int id)
        {
            return _context.DriverVehicle.Any(e => e.DriverVehicleID == id);
        }
    }
}
