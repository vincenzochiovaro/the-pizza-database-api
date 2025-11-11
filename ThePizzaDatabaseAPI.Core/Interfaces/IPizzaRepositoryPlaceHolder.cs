using ThePizzaDatabaseAPI.Core.Models;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IPizzaRepositoryPlaceHolder
{
    Task<List<ContractPlaceHolder>> GetAllAsync();
}