using JockeyInsight.WebApp.DbContext;
using JockeyInsight.WebApp.Entities;

namespace JockeyInsight.WebApp.DataAccess.DbCommands.Notes;

public class AddNotesCommand : IAddNotesCommand
{
    private readonly SimpleDbContext _simpleDbContext;

    public AddNotesCommand(SimpleDbContext simpleDbContext)
    {
        _simpleDbContext = simpleDbContext;
    }

    public async Task AddNotes(Note note)
    {
        note.CreatedAt = DateTime.UtcNow;
        note.UpdatedAt = DateTime.UtcNow;
        _simpleDbContext.Notes.Add(note);
        await _simpleDbContext.SaveChangesAsync();
    }
}

public interface IAddNotesCommand
{
    Task AddNotes(Note note);
}