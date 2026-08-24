using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

public class CalculateReminderSchedule : ICalculateReminderSchedule
{
    public ReminderSchedule CalculateTimings(string date, string time, string preset)
    {
        var now = DateTime.UtcNow;
        return new ReminderSchedule()
        {
            FirstRoundTime = now,
            SecondRoundTime = now.AddMinutes(1),
            ThirdRoundTime = now.AddMinutes(2)
        };
    }
}

// todo - implement logic. + unit tests