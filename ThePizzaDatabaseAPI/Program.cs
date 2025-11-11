using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThePizzaDatabaseAPI;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Infrastructure;
using ThePizzaDatabaseAPI.Infrastructure.Utility;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.UseMiddleware<ApiKeyMiddleware>();

builder.Services.AddOpenTelemetry()
    .UseFunctionsWorkerDefaults()
    .UseAzureMonitorExporter();

MongoConventions.RegisterConventions();

builder.Services.AddSingleton<IPizzaRepositoryPlaceHolder, MongoPizzaRepositoryPlaceHolder>();
builder.Build().Run();