using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;

namespace ThePizzaDatabaseAPI.Core.Services;

public class PizzaService
{
    private readonly IPizzaRepository _repository;

    public PizzaService(IPizzaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PizzaDomain>> GetPizzasByFilter(PizzaFilter filter, string lang)
    {
        if (filter == PizzaFilter.AllPizzas)
            return await _repository.GetAllAsync(lang);

        if (filter == PizzaFilter.VegetarianPizzas)
            return await _repository.GetVegPizzasAsync(lang);

        if (filter == PizzaFilter.WhitePizzas)
            return await _repository.GetWhitePizzasAsync(lang);

        return await _repository.GetAllAsync(lang);
    }
}