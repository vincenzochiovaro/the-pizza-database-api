using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

public class CalculateReminderSchedule : ICalculateReminderSchedule
{
    private static readonly TimeSpan DirectFirstRoundOffset =
        TimeSpan.FromHours(8);

    private static readonly TimeSpan DirectSecondRoundOffset =
        TimeSpan.FromHours(4);

    private static readonly TimeSpan BigaFirstRoundOffset =
        TimeSpan.FromHours(18);

    private static readonly TimeSpan BigaSecondRoundOffset =
        TimeSpan.FromHours(3);

    private static readonly TimeSpan ExpressFirstRoundOffset =
        TimeSpan.FromHours(3);

    private static readonly TimeSpan ExpressSecondRoundOffset =
        TimeSpan.FromHours(2);

    public ReminderScheduleDomain CalculateTimings(
        string date,
        string time,
        PizzaPreset preset)
    {
        var bakingTime = ParseBakingTime(date, time);

        var offsets = GetOffsetsByPreset(preset);

        var firstRoundTime = bakingTime.Subtract(offsets.FirstRoundOffset);
        var secondRoundTime = bakingTime.Subtract(offsets.SecondRoundOffset);

        ValidateSchedule(
            firstRoundTime,
            secondRoundTime,
            bakingTime);

        return new ReminderScheduleDomain
        {
            FirstRoundTime = firstRoundTime,
            SecondRoundTime = secondRoundTime,
            ThirdRoundTime = bakingTime
        };
    }

    private static DateTime ParseBakingTime(string date, string time)
    {
        if (!DateTime.TryParse(
                $"{date} {time}",
                out var bakingTime))
        {
            throw new ArgumentException(
                "The date or time is not valid.");
        }

        return bakingTime;
    }

    private static (TimeSpan FirstRoundOffset, TimeSpan SecondRoundOffset) GetOffsetsByPreset(PizzaPreset preset)
    {
        return preset switch
        {
            PizzaPreset.Direct =>
                (DirectFirstRoundOffset, DirectSecondRoundOffset),

            PizzaPreset.Biga =>
                (BigaFirstRoundOffset, BigaSecondRoundOffset),

            PizzaPreset.Express =>
                (ExpressFirstRoundOffset, ExpressSecondRoundOffset),

            _ => throw new ArgumentOutOfRangeException(
                nameof(preset),
                preset,
                "Unsupported pizza preset.")
        };
    }

    private static void ValidateSchedule(
        DateTime firstRoundTime,
        DateTime secondRoundTime,
        DateTime thirdRoundTime)
    {
        if (firstRoundTime >= secondRoundTime)
        {
            throw new InvalidOperationException(
                "First round time must be before second round time.");
        }

        if (secondRoundTime >= thirdRoundTime)
        {
            throw new InvalidOperationException(
                "Second round time must be before third round time.");
        }
    }
}