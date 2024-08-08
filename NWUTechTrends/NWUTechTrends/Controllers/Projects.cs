﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWUTechTrends.Models;

namespace NWUTechTrends.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Projects : ControllerBase
    {
        private readonly ZaazrNwutechTrendsContext _context;

        public Projects(ZaazrNwutechTrendsContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutProject(Guid id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }

        [HttpGet("GetSavings")]
        public async Task<ActionResult<SavingsResult>> GetSavings(Guid projectId, DateTime startDate, DateTime endDate)
        {
            var telemetryData = await _context.JobTelemetries
                .Where(t => t.ProjectId == projectId && t.EntryDate >= startDate && t.EntryDate <= endDate)
                .ToListAsync();

            if (!telemetryData.Any())
            {
                return NotFound("No telemetry data found for the given project and date range.");
            }

            var totalSavedTime = telemetryData.Sum(t => t.TimeSaved);
            var totalSavedCost = telemetryData.Sum(t => t.CostSaved);

            var result = new SavingsResult
            {
                ProjectId = projectId,
                TotalTimeSaved = totalSavedTime,
                TotalCostSaved = totalSavedCost
            };

            return Ok(result);
        }
    }

    public class SavingsResult
    {
        public Guid ProjectId { get; set; }
        public double TotalTimeSaved { get; set; }
        public decimal TotalCostSaved { get; set; }
    }

}

