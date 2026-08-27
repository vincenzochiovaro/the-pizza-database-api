using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IPizzaRepository
{
    Task<List<PizzaDomain>> GetAllAsync(string lang);
    Task<List<PizzaDomain>> GetVegPizzasAsync(string lang);
    Task<List<PizzaDomain>> GetWhitePizzasAsync(string lang);
}