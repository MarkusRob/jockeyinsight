namespace JockeyInsight.WebApp.Entities;

public class Note
{
    public uint Id { get; set; }

    public uint RaceStatId { get; set; }
    public RaceStat RaceStat { get; set; }

    public string Body { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
