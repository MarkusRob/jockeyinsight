using JockeyInsight.WebApp.DataAccess.Queries.RaceStats;
using JockeyInsight.WebApp.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JockeyInsight.WebApp.Controllers.V1;
[ApiController]
[Route("api/v1/[controller]")]
public class RaceResultsController : Controller
{
    private readonly IRaceResultsQuery _raceResultsQuery;

    public RaceResultsController(IRaceResultsQuery raceResultsQuery)
    {
        _raceResultsQuery = raceResultsQuery;
    }

    [HttpGet]
    public async Task<IActionResult> GetRaceResults([FromQuery] RaceResultQuery query)
    {
        var resultsQuery = _raceResultsQuery.GetAllRaceResults(query);

        var total = await resultsQuery.CountAsync();

        var items = await resultsQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();
        
        return Ok(new
        {
            totalCount = total,
            pageNumber = query.PageNumber,
            pageSize = query.PageSize,
            items
        });
    }
}