using ThePizzaDatabaseAPI.Core.Models;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IPizzaRepository
{
    Task<List<testModel>> GetAllAsync();
}