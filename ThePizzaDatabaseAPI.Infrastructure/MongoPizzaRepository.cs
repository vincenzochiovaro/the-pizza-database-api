using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Models;

namespace ThePizzaDatabaseAPI.Infrastructure;

public class MongoPizzaRepository : IPizzaRepository
{
    public Task<List<testModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}