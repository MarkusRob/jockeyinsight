using JockeyInsight.WebApp.DbContext;
using JockeyInsight.WebApp.Representations.Responses;
using Microsoft.EntityFrameworkCore;

namespace JockeyInsight.WebApp.DataAccess.Queries.Insights;

public class InsightQuery : IInsightQuery
{
    private readonly SimpleDbContext _context;

    public InsightQuery(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task<List<JockeyPerformanceResponse>> GetJockeyStatsForHorse(string horseName)
    {
        return await _context.RaceStats
            .Where(rs => rs.Horse == horseName || rs.Horse.Contains(horseName))
            .GroupBy(rs => rs.Jockey)
            .Select(g => new JockeyPerformanceResponse
            {
                Jockey = g.Key,
                TotalRides = g.Count(),
                Wins = g.Count(rs => rs.FinishingPosition == 1),
                WinRate = Math.Round((double)g.Count(rs => rs.FinishingPosition == 1) / g.Count() * 100, 2),
                AveragePosition = Math.Round(g.Average(rs => rs.FinishingPosition), 2)
            })
            .OrderByDescending(x => x.WinRate)
            .ToListAsync();
    }
    public async Task<bool> HorseNameCheck(string horseName)
    {
        var data = await _context.RaceStats .Where(rs => rs.Horse == horseName || rs.Horse.Contains(horseName)).ToListAsync();
        if (!data.Any())
        {
            return false;
        }

        return true;
    }
}

public interface IInsightQuery
{
    Task<List<JockeyPerformanceResponse>> GetJockeyStatsForHorse(string horseName);
    Task<bool> HorseNameCheck(string horseName);
}