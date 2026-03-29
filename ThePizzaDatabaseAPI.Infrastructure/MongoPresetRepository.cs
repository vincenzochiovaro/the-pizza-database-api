using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Infrastructure.Models;

namespace ThePizzaDatabaseAPI.Infrastructure;

public class MongoPresetRepository : IPresetRepository
{
    private readonly IMongoCollection<PresetDocument> _presetsCollection;

    public MongoPresetRepository(IMongoClient mongoClient)
    {
        var db = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));
        _presetsCollection = db.GetCollection<PresetDocument>("presetSteps");
    }

    public async Task<List<string>> GetStepsByPresetAndLang(string presetTitle, string lang)
    {
        var filter = Builders<PresetDocument>.Filter.Eq(p => p.Title, presetTitle);
        var preset = await _presetsCollection.Find(filter).FirstOrDefaultAsync();

        if (preset is null)
            return new List<string>();

        return lang.ToLower() == "it"
            ? preset.Steps.It
            : preset.Steps.En;
    }
}