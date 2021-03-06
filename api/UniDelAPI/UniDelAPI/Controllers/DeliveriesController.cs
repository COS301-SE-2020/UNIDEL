﻿using System;
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
    public class DeliveriesController : ControllerBase
    {
        private readonly UniDelDbContext _context;

        public DeliveriesController(UniDelDbContext context)
        {
            _context = context;
        }

        // GET: api/Deliveries
        [Route("~/api/Deliveries/GetAllDeliveries")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries([FromQuery] string k="")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key"});
            }

            return await _context.Deliveries.ToListAsync();
        }

        // GET: api/Deliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return delivery;
        }

        // PUT: api/Deliveries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Route("~/api/Deliveries/PutDelivery/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, Delivery delivery, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            if (id != delivery.DeliveryID)
            {
                return BadRequest();
            }

            _context.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
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

        // POST: api/Deliveries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDelivery", new { id = delivery.DeliveryID }, delivery);
        }

        // DELETE: api/Deliveries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Delivery>> DeleteDelivery(int id, [FromQuery] string k = "")
        {
            if (k != "UDL2Avv378jBBgd772hFSbbsfwUD")
            {
                return Unauthorized(new { message = "Request requires a valid API key" });
            }

            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return delivery;
        }

        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.DeliveryID == id);
        }
    }
}
