using ClosedXML.Excel;
using JockeyInsight.WebApp.DbContext;
using JockeyInsight.WebApp.Entities;

namespace JockeyInsight.WebApp.Services;

public class UploadService : IUploadService
{
    private readonly SimpleDbContext _dbContext;

    public UploadService(SimpleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(bool Success, string Message, int Count)> ImportRaceStatsFromXlsxAsync(Stream fileStream)
    {
        try
        {
            fileStream.Position = 0;
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheets.First();

            var stats = new List<RaceStat>();
            foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header
            {
                try
                {
                    var stat = new RaceStat
                    {
                        Race = row.Cell(1).GetString(),
                        RaceDate = row.Cell(2).GetDateTime(),
                        RaceTime = row.Cell(3).GetString(),
                        RaceCourse = row.Cell(4).GetString(),
                        RaceDistance = int.Parse(row.Cell(5).GetValue<string>()),
                        Jockey = row.Cell(6).GetString(),
                        Trainer = row.Cell(7).GetString(),
                        Horse = row.Cell(8).GetString(),
                        FinishingPosition = int.Parse(row.Cell(9).GetValue<string>()),
                        DistanceBeaten = decimal.Parse(row.Cell(10).GetValue<string>()),
                        TimeBeaten = decimal.Parse(row.Cell(11).GetValue<string>())
                    };
                    stats.Add(stat);
                }
                catch (Exception ex)
                {
                    return (false, $"Error parsing row: {ex.Message}", 0);
                }
            }

            await _dbContext.RaceStats.AddRangeAsync(stats);
            await _dbContext.SaveChangesAsync();

            return (true, "Upload successful", stats.Count);
        }
        catch (Exception ex)
        {
            return (false, $"Unexpected error: {ex.Message}", 0);
        }
    }
}

public interface IUploadService
{
    Task<(bool Success, string Message, int Count)> ImportRaceStatsFromXlsxAsync(Stream fileStream);
}