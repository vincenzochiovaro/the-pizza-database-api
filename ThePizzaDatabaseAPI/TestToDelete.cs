using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThePizzaDatabaseAPI;

public class TestToDelete(ILogger<TestToDelete> logger)
{
    [Function("TestToDelete")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        logger.LogInformation("✅ API Key validated - Access granted!");
        return new OkObjectResult("Welcome to PROTECTED function!");
        
    }
}