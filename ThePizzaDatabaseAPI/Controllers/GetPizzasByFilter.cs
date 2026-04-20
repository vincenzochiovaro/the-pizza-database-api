using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThePizzaDatabaseAPI.Core.Enums;
using ThePizzaDatabaseAPI.Core.Services;

namespace ThePizzaDatabaseAPI.Controllers;

public class GetPizzasByFilter(ILogger<GetPizzasByFilter> logger, PizzaService pizzaService)
{
    [Function("GetPizzasByFilter")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        try
        {
            var filter = req.Query["filter"].ToString();
            var lang = req.Query["lang"].ToString();

            if (string.IsNullOrEmpty(filter))
                return new BadRequestObjectResult("filter is required");

            var sanitizedFilter = filter.Replace(" ", "");

            if (!Enum.TryParse<PizzaFilter>(sanitizedFilter, ignoreCase: true, out var pizzaFilter))
                pizzaFilter = PizzaFilter.AllPizzas;

            var result = await pizzaService.GetPizzasByFilter(pizzaFilter, lang);

            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }
}