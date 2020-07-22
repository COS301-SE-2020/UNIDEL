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
    public class CompanyDeliveriesController : ControllerBase
    {
        private readonly UniDelDbContext _context;

        public CompanyDeliveriesController(UniDelDbContext context)
        {
            _context = context;
        }

        // GET: api/CompanyDeliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDelivery>>> GetCompanyDeliveries()
        {
            return await _context.CompanyDeliveries.ToListAsync();
        }

        // GET: api/CompanyDeliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDelivery>> GetCompanyDelivery(int id)
        {
            var companyDelivery = await _context.CompanyDeliveries.FindAsync(id);

            if (companyDelivery == null)
            {
                return NotFound();
            }

            return companyDelivery;
        }

        // PUT: api/CompanyDeliveries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyDelivery(int id, CompanyDelivery companyDelivery)
        {
            if (id != companyDelivery.CompanyDeliveryID)
            {
                return BadRequest();
            }

            _context.Entry(companyDelivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyDeliveryExists(id))
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

        // POST: api/CompanyDeliveries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CompanyDelivery>> PostCompanyDelivery(CompanyDelivery companyDelivery)
        {
            _context.CompanyDeliveries.Add(companyDelivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyDelivery", new { id = companyDelivery.CompanyDeliveryID }, companyDelivery);
        }

        // DELETE: api/CompanyDeliveries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyDelivery>> DeleteCompanyDelivery(int id)
        {
            var companyDelivery = await _context.CompanyDeliveries.FindAsync(id);
            if (companyDelivery == null)
            {
                return NotFound();
            }

            _context.CompanyDeliveries.Remove(companyDelivery);
            await _context.SaveChangesAsync();

            return companyDelivery;
        }

        private bool CompanyDeliveryExists(int id)
        {
            return _context.CompanyDeliveries.Any(e => e.CompanyDeliveryID == id);
        }
    }
}
