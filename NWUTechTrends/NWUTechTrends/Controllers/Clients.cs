using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWUTechTrends.Models;

namespace NWUTechTrends.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Clients : ControllerBase


    {
        private readonly ZaazrNwutechTrendsContext _context;

        public Clients(ZaazrNwutechTrendsContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(Guid id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Clients.Add(client);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(client.ClientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }

        [HttpGet("GetSavings")]
        public async Task<ActionResult<ClientSavingsResult>> GetSavings(Guid clientId, DateTime startDate, DateTime endDate)
        {
            var telemetryData = await _context.JobTelemetries
                .Where(t => t.ClientId == clientId && t.EntryDate >= startDate && t.EntryDate <= endDate)
                .ToListAsync();

            if (!telemetryData.Any())
            {
                return NotFound("No telemetry data found for the given client and date range.");
            }

            var totalHoursSaved = telemetryData.Sum(t => t.TimeSaved);
            var totalCostReduction = telemetryData.Sum(t => t.CostSaved);

            var result = new ClientSavingsResult
            {
                ClientIdentifier = clientId,
                HoursSaved = totalHoursSaved,
                CostReduction = totalCostReduction
            };

            return Ok(result);
        }
    }

    public class ClientSavingsResult
    {
        public Guid ClientIdentifier { get; set; }
        public double HoursSaved { get; set; }
        public decimal CostReduction { get; set; }
    }
}


