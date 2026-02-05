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

    public async Task<List<Pizza>> GetPizzasByFilter(PizzaFilter filter, string lang)
    {
        if (filter == PizzaFilter.AllPizzas)
            return await _repository.GetAllAsync(lang);

        if (filter == PizzaFilter.VegetarianPizzas)
            return await _repository.GetVegPizzasAsync(lang);

        if (filter == PizzaFilter.StuffedCrustPizzas)
            return await _repository.GetStuffedCrustPizzasAsync(lang);

        return await _repository.GetAllAsync(lang);
    }
}