using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Contracts;

namespace ThePizzaDatabaseAPI.Infrastructure;

public class MongoPizzaRepository : IPizzaRepository
{
    IMongoCollection<Pizza> _pizzasCollection;
    public MongoPizzaRepository(IMongoClient mongoClient)
    {
        var db = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));

        _pizzasCollection = db.GetCollection<Pizza>("pizzas");
    }
    
    public async Task<List<Pizza>> GetAllAsync()
    {
        var allPizzas = await _pizzasCollection.Find(_ => true).ToListAsync();
        return allPizzas.OrderBy(_ => Random.Shared.Next()).ToList();
    }

    public async Task<List<Pizza>> GetVegPizzasAsync()
    {
        var filter = Builders<Pizza>.Filter.Eq(pizza => pizza.IsVegetarian, true);

        var vegPizzas = await _pizzasCollection.Find(filter).ToListAsync();
        return vegPizzas.OrderBy(_ => Random.Shared.Next()).ToList();
    }
}