using JockeyInsight.WebApp.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JockeyInsight.WebApp.Controllers.V1;
[ApiController]
[Route("api/v1/[controller]")]
public class HorsesController: Controller
{
    private readonly SimpleDbContext _context;

    public HorsesController(SimpleDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetHorses()
    {
        /// Add to Service.
        var horses = await _context.RaceStats
            .Select(r => r.Horse)
            .Distinct()
            .OrderBy(h => h)
            .ToListAsync();

        return Ok(horses);
    }
}