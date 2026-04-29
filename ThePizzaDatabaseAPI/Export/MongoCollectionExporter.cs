using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ThePizzaDatabaseAPI.Infrastructure.Backup;

namespace ThePizzaDatabaseAPI.Export;

public class MongoCollectionExporter(
    ILogger<MongoCollectionExporter> logger,   
    IMongoBackupService backupService)
{
    [Function("WeeklyMongoExport")]
    public async Task Run([TimerTrigger("0 5 * * 3", RunOnStartup = true)] TimerInfo timer)
    {
        logger.LogInformation("Weekly export job triggered at: {TriggerTime}", DateTime.UtcNow);
        
        await backupService.ExportAllCollectionsAsync();
    }
}