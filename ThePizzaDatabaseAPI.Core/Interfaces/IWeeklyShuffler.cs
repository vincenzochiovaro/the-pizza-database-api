using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IWeeklyShuffler
{
    List<Pizza> Shuffle(List<Pizza> pizzas);
}