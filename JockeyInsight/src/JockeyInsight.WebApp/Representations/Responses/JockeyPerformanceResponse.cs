namespace JockeyInsight.WebApp.Representations.Responses;

public class JockeyPerformanceResponse
{
    public string Jockey { get; set; }
    public int TotalRides { get; set; }
    public int Wins { get; set; }
    public double WinRate { get; set; }
    public double AveragePosition { get; set; }
}