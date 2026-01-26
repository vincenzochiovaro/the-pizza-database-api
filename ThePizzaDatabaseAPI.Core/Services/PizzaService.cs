using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Core.Enums;

namespace ThePizzaDatabaseAPI.Core.Services;

public class PizzaService
{
    private readonly IPizzaRepository _repository;

    public PizzaService(IPizzaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Pizza>> GetPizzasByFilter(PizzaFilter filter)
    {
        if (filter == PizzaFilter.AllPizzas)
            return await _repository.GetAllAsync();

        if (filter == PizzaFilter.VegetarianPizzas)
            return await _repository.GetVegPizzasAsync(); 

        return await _repository.GetAllAsync();
    }
}