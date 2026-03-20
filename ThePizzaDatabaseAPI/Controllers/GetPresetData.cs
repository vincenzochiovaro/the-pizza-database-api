using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThePizzaDatabaseAPI.Controllers;

public class GetPresetData
{
    private readonly ILogger<GetPresetData> _logger;

    public GetPresetData(ILogger<GetPresetData> logger)
    {
        _logger = logger;
    }

    [Function("GetPresetData")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        var preset = req.Query["preset"].ToString();
        var lang = req.Query["lang"].ToString();
        var doughBallCountRaw = req.Query["doughBallCount"].ToString();
        var doughBallWeightRaw = req.Query["doughBallWeight"].ToString();

        int.TryParse(doughBallCountRaw, out var doughBallCount);
        int.TryParse(doughBallWeightRaw, out var doughBallWeight);

        return new OkObjectResult(null);
    }
}