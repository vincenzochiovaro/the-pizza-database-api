using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThePizzaDatabaseAPI.Core.Services;
using ThePizzaDatabaseAPI.Models.Requests;
using ThePizzaDatabaseAPI.Models.Responses;

namespace ThePizzaDatabaseAPI.Controllers;

public class GetPresetData(ILogger<GetPresetData> logger, PresetsDataService presetsDataService)
{
    [Function("GetPresetData")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        try
        {
            var request = MapRequest(req);

            if (request == null)
                return new BadRequestObjectResult("Invalid request parameters");

            var result = await presetsDataService.GetPresetDataAsync(
                request.Preset,
                request.Lang,
                request.DoughBallCount,
                request.DoughBallWeight,
                request.Hydration,
                request.Temperature,
                request.Preferment);

            var response = new GetPresetResponse
            {
                Water = result.Water,
                Flour = result.Flour,
                Salt = result.Salt,
                WaterDay2 = result.WaterDay2,
                FlourDay2 = result.FlourDay2,
                SaltDay2 = result.SaltDay2,
                Yeast = result.Yeast,
                Steps = result.Steps,
                Tips = result.Tips
            };

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    private static GetPresetDataRequest? MapRequest(HttpRequest req)
    {
        int.TryParse(req.Query["doughBallCount"], out var doughBallCount);
        int.TryParse(req.Query["doughBallWeight"], out var doughBallWeight);
        int.TryParse(req.Query["hydration"], out var hydration);
        int.TryParse(req.Query["temperature"], out var temperature);

        if (doughBallCount <= 0 || doughBallCount > 20)
        {
            return null;
        }

        int? preferment = null;
        if (int.TryParse(req.Query["preferment"], out var parsedPreferment))
        {
            preferment = parsedPreferment;
        }

        return new GetPresetDataRequest
        {
            Preset = req.Query["preset"],
            Lang = req.Query["lang"],
            DoughBallCount = doughBallCount,
            DoughBallWeight = doughBallWeight,
            Hydration = hydration,
            Temperature = temperature,
            Preferment = preferment
        };
    }
}