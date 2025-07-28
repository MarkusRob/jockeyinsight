using JockeyInsight.WebApp.DbContext;
using JockeyInsight.WebApp.QueryFilters;
using JockeyInsight.WebApp.Representations.Responses;
using Microsoft.EntityFrameworkCore;

namespace JockeyInsight.WebApp.DataAccess.Queries.RaceStats;

public class RaceResultsQuery : IRaceResultsQuery
{
    private SimpleDbContext _context;

    public RaceResultsQuery(SimpleDbContext context)
    {
        _context = context;
    }

    public IQueryable<RaceStatsResponse> GetAllRaceResults(RaceResultQuery query)
    {
        var baseQuery = _context.RaceStats
            .Include(rs => rs.Note)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.HorseName))
        {
            var term = query.HorseName.ToLower();
            baseQuery = baseQuery.Where(rs => rs.Horse.ToLower().Contains(term)
            );
        }
        if (!string.IsNullOrWhiteSpace(query.RaceName))
        {
            var term = query.RaceName.ToLower();
            baseQuery = baseQuery.Where(rs => rs.Race.ToLower().Contains(term)
            );
        }
        
        if (!string.IsNullOrWhiteSpace(query.RaceCourse))
        {
            var term = query.RaceCourse.ToLower();
            baseQuery = baseQuery.Where(rs => rs.RaceCourse.ToLower().Contains(term));
        }
        else
        {
            baseQuery = baseQuery.OrderBy(rs => rs.RaceCourse);
        }
        var projectedQuery = baseQuery.Select(rs => new RaceStatsResponse
        {
            Id = rs.Id,
            Race = rs.Race,
            RaceDate = rs.RaceDate,
            RaceTime = rs.RaceTime,
            RaceCourse = rs.RaceCourse,
            RaceDistance = rs.RaceDistance,
            Horse = rs.Horse,
            Jockey = rs.Jockey,
            Trainer = rs.Trainer,
            FinishingPosition = rs.FinishingPosition,
            DistanceBeaten = rs.DistanceBeaten,
            TimeBeaten = rs.TimeBeaten,
            Note = rs.Note
        });

        return projectedQuery;
    }
}

public interface IRaceResultsQuery
{ 
    IQueryable<RaceStatsResponse> GetAllRaceResults(RaceResultQuery query);
}