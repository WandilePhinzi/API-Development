using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWUTechTrends.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWUTechTrends.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly ZaazrNwutechTrendsContext _context;

        public TelemetryController(ZaazrNwutechTrendsContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetMethod")]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetTelemetry()
        {
            return await _context.JobTelemetries.ToListAsync();
        }
    }
}
