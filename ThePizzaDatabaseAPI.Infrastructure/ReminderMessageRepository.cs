using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Infrastructure.Mappers;
using ThePizzaDatabaseAPI.Infrastructure.Models;

namespace ThePizzaDatabaseAPI.Infrastructure;

public class ReminderMessageRepository : IReminderMessageRepository
{
    private readonly IMongoCollection<ReminderMessageDocument> _presetsCollection;

    public ReminderMessageRepository(IMongoClient mongoClient)
    {
        var db = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));
        _presetsCollection = db.GetCollection<ReminderMessageDocument>("reminderMessages");
    }

    public async Task<ReminderMessageDomain> GetByPresetAsync(string preset)
    {
        var reminderDocument = await _presetsCollection
            .Find(x => x.Preset == preset)
            .FirstOrDefaultAsync();

        return ReminderMessageMapper.ToDomain(reminderDocument);
    }
}