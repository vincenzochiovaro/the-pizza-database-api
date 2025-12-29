using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThePizzaDatabaseAPI.Core.Services;

namespace ThePizzaDatabaseAPI.Controllers;

public class GetPizzasByFilter
{
    private readonly ILogger<GetPizzasByFilter> _logger;
    private readonly PizzaService _pizzaService;

    public GetPizzasByFilter(ILogger<GetPizzasByFilter> logger, PizzaService  pizzaService)
    {
        _logger = logger;
        _pizzaService = pizzaService;
        
    }

    [Function("GetPizzasByFilter")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        var filter = req.Query["filter"].ToString();

        if (string.IsNullOrEmpty(filter))
        {
            return new BadRequestObjectResult("filter is required");
        }
        
        var result = await _pizzaService.GetPizzasByFilter(filter);
        return new OkObjectResult(result);
    }
}