using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ThePizzaDatabaseAPI.Infrastructure.Backup;

public class MongoBackupService : IMongoBackupService
{
    private readonly IMongoDatabase _database;

    public MongoBackupService(IMongoClient mongoClient)
    {
        _database = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));
    }

    public async Task ExportAllCollectionsAsync()
    {
        var collections = await _database.ListCollectionNames().ToListAsync();

        foreach (var collectionName in collections)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            var json = await BuildJsonAsync(collection);
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