using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThePizzaDatabaseAPI;
using Azure.Monitor.OpenTelemetry.Exporter;
using Azure.Storage.Blobs;
using brevo_csharp.Client;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Services;
using ThePizzaDatabaseAPI.Infrastructure;
using ThePizzaDatabaseAPI.Infrastructure.Backup;
using ThePizzaDatabaseAPI.Infrastructure.Utilities;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.UseMiddleware<ApiKeyMiddleware>();
MongoConventions.RegisterConventions();

builder.Services.AddOpenTelemetry()
    .UseFunctionsWorkerDefaults()
    .UseAzureMonitorExporter();

Configuration.Default.ApiKey.Add("api-key", builder.Configuration["BrevoApi:ApiKey"]);

builder.Services.AddSingleton<IPresetRepository, MongoPresetRepository>();
builder.Services.AddSingleton<IMongoBackupService, MongoBackupService>();
builder.Services.AddScoped<IPizzaRepository, MongoPizzaRepository>();
builder.Services.AddScoped<IWeeklyShuffler>(_ => new WeeklyShuffler(() => DateTime.UtcNow));
builder.Services.AddScoped<PizzaService>();
builder.Services.AddScoped<PresetsDataService>();
builder.Services.AddScoped<ICalculateReminderSchedule, CalculateReminderSchedule>();

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING")));

builder.Services.AddSingleton<BlobServiceClient>(_ =>
    new BlobServiceClient(Environment.GetEnvironmentVariable("BLOB_CONNECTION_STRING")));

builder.Build().Run();