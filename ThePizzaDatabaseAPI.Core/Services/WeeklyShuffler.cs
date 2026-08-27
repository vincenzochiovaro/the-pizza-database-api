using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

/// Provide a stable but rotating order of pizzas
/// The order must stay the same for the whole week
/// change automatically when a new week starts
public class WeeklyShuffler : IWeeklyShuffler
{
    private readonly Func<DateTime> _now;

    public WeeklyShuffler(Func<DateTime>? now)
    {
        _now = now ?? (() => DateTime.UtcNow);
    }

    public List<PizzaDomain> Shuffle(List<PizzaDomain> pizzas)
    {
        var seed = GetWeeklySeed(_now());
        var weeklyOrderGenerator = new Random(seed);

        return pizzas
            .OrderBy(_ => weeklyOrderGenerator.Next())
            .ToList();
    }

    private int GetWeeklySeed(DateTime now)
    {
        var year = now.Year;
        var week = System.Globalization.ISOWeek.GetWeekOfYear(now);

        return year * 100 + week;
    }
}