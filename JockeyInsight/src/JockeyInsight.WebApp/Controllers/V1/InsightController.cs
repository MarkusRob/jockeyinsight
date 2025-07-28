using JockeyInsight.WebApp.DataAccess.Queries.Insights;
using Microsoft.AspNetCore.Mvc;

namespace JockeyInsight.WebApp.Controllers.V1;
[ApiController]
[Route("api/v1/[controller]")]
public class InsightController: Controller
{
    private readonly IInsightQuery  _insightQuery;

    public InsightController(IInsightQuery insightQuery)
    {
        _insightQuery = insightQuery;
    }

    [HttpGet("jockey_performance")]
    public async Task<IActionResult> GetJockeyPerformance([FromQuery] string horse)
    {
        if (string.IsNullOrWhiteSpace(horse))
            return BadRequest("Horse name is required.");

        var horseExists = await _insightQuery.HorseNameCheck(horse);
        if (!horseExists) return NotFound("Horse name does not exist.");
        var result = await _insightQuery.GetJockeyStatsForHorse(horse);
        return Ok(result);
    }
}