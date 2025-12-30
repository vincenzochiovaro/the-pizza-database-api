using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Contracts;

namespace ThePizzaDatabaseAPI.Infrastructure;

public class MongoPizzaRepository : IPizzaRepository
{
    IMongoCollection<Pizza> _collectionWithTestModel;
    public MongoPizzaRepository(IMongoClient mongoClient)
    {
        var db = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));

        _collectionWithTestModel = db.GetCollection<Pizza>("pizzas");
    }
    
    public async Task<List<Pizza>> GetAllAsync()
    {
        var allPizzas = await _collectionWithTestModel.Find(_ => true).ToListAsync();
        return allPizzas;
    }
}