using JockeyInsight.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace JockeyInsight.WebApp.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class UploadController : Controller
{
    private readonly IUploadService _uploadService;

    public UploadController(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file == null || file.Length == 0 || !file.FileName.EndsWith(".xlsx")) return BadRequest("Invalid file format. Only .xlsx files are accepted.");

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        var result = await _uploadService.ImportRaceStatsFromXlsxAsync(stream);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok();
    }
}