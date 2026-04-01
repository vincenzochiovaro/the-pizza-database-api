using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThePizzaDatabaseAPI.Core.Services;
using ThePizzaDatabaseAPI.Models.Requests;
using ThePizzaDatabaseAPI.Models.Responses;

namespace ThePizzaDatabaseAPI.Controllers;

public class GetPresetData
{
    private readonly ILogger<GetPresetData> _logger;
    private readonly PresetsDataService _presetsDataService;

    public GetPresetData(ILogger<GetPresetData> logger, PresetsDataService presetsDataService)
    {
        _logger = logger;
        _presetsDataService = presetsDataService;
    }

    [Function("GetPresetData")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        try
        {
            var request = MapRequest(req);
            var result = await _presetsDataService.GetPresetDataAsync(
                request.Preset, request.Lang, request.DoughBallCount, request.DoughBallWeight);

            var response = new GetPresetResponse
            {
                Water = result.Water,
                Flour = result.Flour,
                Salt = result.Salt,
                Yeast = result.Yeast,
                Steps = result.Steps
            };

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    private static GetPresetDataRequest MapRequest(HttpRequest req)
    {
        int.TryParse(req.Query["doughBallCount"], out var doughBallCount);
        int.TryParse(req.Query["doughBallWeight"], out var doughBallWeight);

        return new GetPresetDataRequest
        {
            Preset = req.Query["preset"],
            Lang = req.Query["lang"],
            DoughBallCount = doughBallCount,
            DoughBallWeight = doughBallWeight
        };
    }
}