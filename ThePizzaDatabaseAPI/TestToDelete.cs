using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI;

public class TestToDelete(ILogger<TestToDelete> logger, IPizzaRepository  pizzaRepository)
{
    [Function("TestToDelete")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        logger.LogInformation("deployed via devops Access granted!");
        pizzaRepository.GetAllAsync();
        return new OkObjectResult("🚀Welcome to PROTECTED function!");
        
    }
}