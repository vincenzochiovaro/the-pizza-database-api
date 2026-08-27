using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IWeeklyShuffler
{
    List<PizzaDomain> Shuffle(List<PizzaDomain> pizzas);
}