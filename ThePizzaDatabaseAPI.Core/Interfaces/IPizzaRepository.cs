using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IPizzaRepository
{
    Task<List<Pizza>> GetAllAsync(string lang);
    Task<List<Pizza>> GetVegPizzasAsync(string lang);
    Task<List<Pizza>> GetWhitePizzasAsync(string lang);
}