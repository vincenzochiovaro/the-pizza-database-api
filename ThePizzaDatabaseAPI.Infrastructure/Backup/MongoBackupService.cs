using System.Text;
using Azure.Storage.Blobs;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ThePizzaDatabaseAPI.Infrastructure.Backup;

public class MongoBackupService(IMongoClient mongoClient, BlobServiceClient blobServiceClient) : IMongoBackupService
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));
    private readonly BlobContainerClient _blobContainerClient = blobServiceClient.GetBlobContainerClient("thepizzadatabasebackup");

    public async Task ExportAllCollectionsAsync()
    {
        var collections = await _database.ListCollectionNames().ToListAsync();

        foreach (var collectionName in collections)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            
            var json = await BuildJsonAsync(collection);

            var fileName = $"{collectionName}-{DateTime.UtcNow:yyyyMMdd}.json";

            var blobClient = _blobContainerClient.GetBlobClient(fileName);

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            await blobClient.UploadAsync(stream, overwrite: true);
        }
    }
    
    private async Task<string> BuildJsonAsync(IMongoCollection<BsonDocument> collection)
    {
        var cursor = await collection.Find(FilterDefinition<BsonDocument>.Empty).ToCursorAsync();

        var sb = new StringBuilder();
        sb.Append("[");
        var first = true;

        while (await cursor.MoveNextAsync())
        {
            foreach (var doc in cursor.Current)
            {
                if (!first) sb.Append(",");
                sb.Append(doc.ToJson());
                first = false;
            }
        }

        sb.Append("]");
        return sb.ToString();
    }
}