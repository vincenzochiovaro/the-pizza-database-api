using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThePizzaDatabaseAPI.Core.Services;

namespace ThePizzaDatabaseAPI.Controllers;

public class GetPresetData
{
    private readonly ILogger<GetPresetData> _logger;
    private readonly PresetDoughBuilderService _presetDoughBuilderService;

    public GetPresetData(ILogger<GetPresetData> logger, PresetDoughBuilderService presetDoughBuilderService)
    {
        _logger = logger;
        _presetDoughBuilderService = presetDoughBuilderService;
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

        var result = await _presetDoughBuilderService.GetDoughIngredients(
            preset,
            lang,
            doughBallCount,
            doughBallWeight);

        return new OkObjectResult(result);
    }
}