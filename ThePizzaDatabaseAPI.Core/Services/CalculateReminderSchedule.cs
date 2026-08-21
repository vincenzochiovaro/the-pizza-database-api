using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

public class CalculateReminderSchedule : ICalculateReminderSchedule
{
    public ReminderSchedule CalculateTimings(string date, string time, string preset)
    {
        return new ReminderSchedule()
        {
            FirstRoundTime = new DateTime(),
            SecondRoundTime = new DateTime(),
            ThirdRoundTime = new DateTime()
        };
    }
}