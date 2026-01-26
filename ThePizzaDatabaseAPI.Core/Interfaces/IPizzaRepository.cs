using ThePizzaDatabaseAPI.Core.Contracts;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IPizzaRepository
{
    Task<List<Pizza>> GetAllAsync();
    Task<List<Pizza>> GetVegPizzasAsync();
}