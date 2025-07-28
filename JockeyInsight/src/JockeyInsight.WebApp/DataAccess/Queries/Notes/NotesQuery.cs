using JockeyInsight.WebApp.DbContext;
using JockeyInsight.WebApp.Entities;

namespace JockeyInsight.WebApp.DataAccess.Queries.Notes;

public class NotesQuery : INotesQuery
{
    private readonly SimpleDbContext _simpleDbContext;

    public NotesQuery(SimpleDbContext simpleDbContext)
    {
        _simpleDbContext = simpleDbContext;
    }

    public IQueryable<Note> GetNotes(uint raceStatId)
    {
        return _simpleDbContext.Notes
            .Where(n => n.RaceStatId == raceStatId)
            .OrderByDescending(n => n.CreatedAt);
    }
}

public interface INotesQuery
{
    IQueryable<Note> GetNotes(uint raceStatId);
}