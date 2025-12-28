using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Contracts;

namespace ThePizzaDatabaseAPI.Core.Services;

public class PizzaService
{
    private readonly IPizzaRepository _repository;

    public PizzaService(IPizzaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Pizza>> GetPizzasByFilter(string filter)
    {
        if (filter == "All pizzas")
            return await _repository.GetAllAsync();

        if (filter == "Preparation time")
            return await _repository.GetAllAsync();

        return await _repository.GetAllAsync();
    }
}