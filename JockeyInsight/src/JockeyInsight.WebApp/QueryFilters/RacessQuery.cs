namespace JockeyInsight.WebApp.QueryFilters;

public class RaceResultQuery
{
    public string? HorseName { get; set; }
    public string? RaceName { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? RaceCourse { get; set; }
}
