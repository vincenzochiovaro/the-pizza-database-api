using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Models;

namespace ThePizzaDatabaseAPI.Infrastructure;

public class MongoPizzaRepositoryPlaceHolder : IPizzaRepositoryPlaceHolder
{
    IMongoCollection<ContractPlaceHolder> _collectionWithTestModel;
    public MongoPizzaRepositoryPlaceHolder()
    {
        var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"));
        var db = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));
        
        _collectionWithTestModel = db.GetCollection<ContractPlaceHolder>("tempPizza"); 
    }
    public async Task<List<ContractPlaceHolder>> GetAllAsync()
    {
        await _collectionWithTestModel.InsertOneAsync(new ContractPlaceHolder(){PizzaName = Guid.NewGuid().ToString()});
        
        var foo = await _collectionWithTestModel.Find(_ => true).ToListAsync();
        return foo;
    }
}