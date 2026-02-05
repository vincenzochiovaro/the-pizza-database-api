using ThePizzaDatabaseAPI.Core.Contracts;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IWeeklyShuffler
{
    List<Pizza> Shuffle(List<Pizza> pizzas);
}