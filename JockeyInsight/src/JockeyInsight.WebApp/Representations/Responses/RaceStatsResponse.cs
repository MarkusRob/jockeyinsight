using JockeyInsight.WebApp.Entities;

namespace JockeyInsight.WebApp.Representations.Responses;

public class RaceStatsResponse
{
        public uint Id { get; set; }
        public string Race { get; set; }
        public DateTime RaceDate { get; set; }
        public string RaceTime { get; set; }
        public string RaceCourse { get; set; }
        public int RaceDistance { get; set; }
        public string Jockey { get; set; }
        public string Trainer { get; set; }
        public string Horse { get; set; }
        public int FinishingPosition { get; set; }
        public decimal DistanceBeaten { get; set; }
        public decimal TimeBeaten { get; set; }
        public Note? Note { get; set; }
    
}