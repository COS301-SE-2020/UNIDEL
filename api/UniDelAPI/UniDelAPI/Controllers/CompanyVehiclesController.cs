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
    public class CompanyVehiclesController : ControllerBase
    {
        private readonly UniDelDbContext _context;

        public CompanyVehiclesController(UniDelDbContext context)
        {
            _context = context;
        }

        // GET: api/CompanyVehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyVehicle>>> GetCompanyVehicles([FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            return await _context.CompanyVehicles.ToListAsync();
        }

        // GET: api/CompanyVehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyVehicle>> GetCompanyVehicle(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var companyVehicle = await _context.CompanyVehicles.FindAsync(id);

            if (companyVehicle == null)
            {
                return NotFound();
            }

            return companyVehicle;
        }

        // PUT: api/CompanyVehicles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyVehicle(int id, CompanyVehicle companyVehicle, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            if (id != companyVehicle.CompanyVehicleID)
            {
                return BadRequest();
            }

            _context.Entry(companyVehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyVehicleExists(id))
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

        // POST: api/CompanyVehicles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CompanyVehicle>> PostCompanyVehicle(CompanyVehicle companyVehicle, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            _context.CompanyVehicles.Add(companyVehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyVehicle", new { id = companyVehicle.CompanyVehicleID }, companyVehicle);
        }

        // DELETE: api/CompanyVehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyVehicle>> DeleteCompanyVehicle(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var companyVehicle = await _context.CompanyVehicles.FindAsync(id);
            if (companyVehicle == null)
            {
                return NotFound();
            }

            _context.CompanyVehicles.Remove(companyVehicle);
            await _context.SaveChangesAsync();

            return companyVehicle;
        }

        private bool CompanyVehicleExists(int id)
        {
            return _context.CompanyVehicles.Any(e => e.CompanyVehicleID == id);
        }
    }
}
