using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Infrastructure.Models;

namespace ThePizzaDatabaseAPI.Infrastructure;

public class MongoPresetRepository : IPresetRepository
{
    private readonly IMongoCollection<PresetDocument> _presetsCollection;

    public MongoPresetRepository(IMongoClient mongoClient)
    {
        var db = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));
        _presetsCollection = db.GetCollection<PresetDocument>("presets");
    }

    public async Task<PresetData?> GetByPresetAsync(string preset)
    {
        // Mapping from PresetDocument → PresetData to be implemented once schema is designed
        return await Task.FromResult<PresetData?>(null);
    }
}