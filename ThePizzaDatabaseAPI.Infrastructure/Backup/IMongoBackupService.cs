namespace ThePizzaDatabaseAPI.Infrastructure.Backup;

public interface IMongoBackupService

{
    Task ExportAllCollectionsAsync();
}