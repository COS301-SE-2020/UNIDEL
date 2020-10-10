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
    public class CourierCompaniesController : ControllerBase
    {
        private readonly UniDelDbContext _context;

        public CourierCompaniesController(UniDelDbContext context)
        {
            _context = context;
        }

        // GET: api/CourierCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourierCompany>>> GetCourierCompanies([FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            return await _context.CourierCompanies.ToListAsync();
        }

        // GET: api/CourierCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourierCompany>> GetCourierCompany(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var courierCompany = await _context.CourierCompanies.FindAsync(id);

            if (courierCompany == null)
            {
                return NotFound();
            }

            return courierCompany;
        }

        // PUT: api/CourierCompanies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourierCompany(int id, CourierCompany courierCompany, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            if (id != courierCompany.CourierCompanyID)
            {
                return BadRequest();
            }

            _context.Entry(courierCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourierCompanyExists(id))
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

        // POST: api/CourierCompanies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CourierCompany>> PostCourierCompany(CourierCompany courierCompany, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            _context.CourierCompanies.Add(courierCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourierCompany", new { id = courierCompany.CourierCompanyID }, courierCompany);
        }

        // DELETE: api/CourierCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourierCompany>> DeleteCourierCompany(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var courierCompany = await _context.CourierCompanies.FindAsync(id);
            if (courierCompany == null)
            {
                return NotFound();
            }

            _context.CourierCompanies.Remove(courierCompany);
            await _context.SaveChangesAsync();

            return courierCompany;
        }

        private bool CourierCompanyExists(int id)
        {
            return _context.CourierCompanies.Any(e => e.CourierCompanyID == id);
        }
    }
}
