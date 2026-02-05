using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

public class WeeklyShuffler : IWeeklyShuffler
{
    /// Provide a stable but rotating order of pizzas
    /// The order must stay the same for the whole week
    /// change automatically when a new week starts

    public List<Pizza> Shuffle(List<Pizza> pizzas)
    {
        var seed = GetWeeklySeed();
        var weeklyOrderGenerator = new Random(seed);
        
        return pizzas
            .OrderBy(_ => weeklyOrderGenerator.Next())
            .ToList();
    }

    private int GetWeeklySeed()
    {
        var now = DateTime.UtcNow;

        var year = now.Year;
        var week = System.Globalization.ISOWeek.GetWeekOfYear(now);

        return year * 100 + week;
    }
}