using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace ThePizzaDatabaseAPI;

public class ApiKeyMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ApiKeyMiddleware> _logger;

    public ApiKeyMiddleware(ILogger<ApiKeyMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var req = await context.GetHttpRequestDataAsync();
        if (req == null)
        {
            await next(context);
            return;
        }

        var apiKeyFromRequest = req.Headers.TryGetValues("X-API-Key", out var values);
        var apiKey = apiKeyFromRequest ? values?.FirstOrDefault() : null;

        var secretKey = Environment.GetEnvironmentVariable("PIZZA_DB_API_KEY");

        if (string.IsNullOrEmpty(apiKey) || apiKey != secretKey)
        {
            _logger.LogWarning("Unauthorized request, missing or invalid API key");
            var res = req.CreateResponse(HttpStatusCode.Unauthorized);
            await res.WriteStringAsync("Unauthorized");

            context.GetInvocationResult().Value = res;
            return;
        }

        await next(context);
    }
}