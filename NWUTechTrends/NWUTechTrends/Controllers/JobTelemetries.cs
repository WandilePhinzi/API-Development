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
   // [AllowAnonymous]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobTelemetries : ControllerBase
    {
        private readonly ZaazrNwutechTrendsContext _context;

        public JobTelemetries(ZaazrNwutechTrendsContext context)
        {
            _context = context;
        }

        // GET: api/JobTelemetries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }

        // GET: api/JobTelemetries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        // PUT: api/JobTelemetries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if (id != jobTelemetry.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobTelemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
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

        // POST:api/JobTelemetries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobTelemetry", new { id = jobTelemetry.Id }, jobTelemetry);
        }

        // DELETE:api/JobTelemetries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpGet("savings/project/{projectId}")]
        public IActionResult GetSavingsByProjectId(Guid projectId, DateTime startDate, DateTime endDate)
        {
            try
            {
                // Perform a join between JobTelemetry and Process tables
                var telemetries = from jt in _context.JobTelemetries
                                  where jt.Process.ProjectId == projectId
                                        && jt.EntryDate >= startDate
                                        && jt.EntryDate <= endDate
                                  select jt;


                // Check if ExcludeFromTimeSaving is set for any telemetry
                var filteredTelemetries = telemetries
                    .Where(t => !t.ExcludeFromTimeSaving.HasValue || !t.ExcludeFromTimeSaving.Value);

                var totalTimeSaved = filteredTelemetries.Sum(t => t.HumanTime ?? 200); // Handle potential null HumanTime
                var totalCostSaved = filteredTelemetries.Sum(t => t.CostSaved ?? 4000);

                var savings = new
                {
                    ProjectId = projectId,
                    TotalTimeSaved = totalTimeSaved,
                    TotalCostSaved = totalCostSaved
                };

                return Ok(savings);
            }

            catch
            {
                return StatusCode(500, "An error occurred while calculating savings.");
            }
         }




        [HttpGet("savings/clients/{clientsId}")]
        public IActionResult GetSavingsByClientsId(Guid clientsId, DateTime startDate, DateTime endDate)
        {
            try
            {
                // Perform a join between JobTelemetry and Process tables
                var telemetries = from jt in _context.JobTelemetries
                                  where jt.Process.ClientId == clientsId
                                        && jt.EntryDate >= startDate
                                        && jt.EntryDate <= endDate
                                  select jt;


                // Check if ExcludeFromTimeSaving is set for any telemetry
                var filteredTelemetries = telemetries
                    .Where(t => !t.ExcludeFromTimeSaving.HasValue || !t.ExcludeFromTimeSaving.Value);

                var totalTimeSaved = filteredTelemetries.Sum(t => t.HumanTime ?? 200); // Handle potential null HumanTime
                var totalCostSaved = filteredTelemetries.Sum(t => t.CostSaved ?? 4000);

                var savings = new
                {
                    ClientId = clientsId,
                    TotalTimeSaved = totalTimeSaved,
                    TotalCostSaved = totalCostSaved
                };

                return Ok(savings);
            }

            catch
            {
                return StatusCode(500, "An error occurred while calculating savings.");
            }
        }


        private bool JobTelemetryExists(int id)
        {
            return _context.JobTelemetries.Any(e => e.Id == id);
        }
    }
}
