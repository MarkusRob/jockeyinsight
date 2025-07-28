using JockeyInsight.WebApp.DataAccess.DbCommands.Notes;
using JockeyInsight.WebApp.DataAccess.Queries.Notes;
using JockeyInsight.WebApp.Entities;
using JockeyInsight.WebApp.Representations.Requests.Note;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JockeyInsight.WebApp.Controllers.V1;
[ApiController]
[Route("api/v1/[controller]")]
public class NotesController : Controller
{
    private readonly INotesQuery _notesQuery;
    private readonly IAddNotesCommand _addNotesCommand;

    public NotesController(INotesQuery notesQuery, IAddNotesCommand addNotesCommand)
    {
        _notesQuery = notesQuery;
        _addNotesCommand = addNotesCommand;
    }

    [HttpGet("{raceStatId:int}")]
    public async Task<IActionResult> GetNotesByRaceStatId([FromRoute] uint raceStatId)
    {
        var response = await _notesQuery.GetNotes(raceStatId).ToListAsync();
        if (!response.Any())
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody] NoteRequest note)
    {
        if (note == null)
        {
            return BadRequest();
        }

        await _addNotesCommand.AddNotes(new Note
        {
            RaceStatId = note.RaceStatId,
            Body = note.Body,
        });
        return Ok();
    }
}