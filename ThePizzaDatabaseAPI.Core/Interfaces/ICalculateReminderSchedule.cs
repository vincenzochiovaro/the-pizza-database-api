using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface ICalculateReminderSchedule
{
    ReminderSchedule CalculateTimings(string date, string time, string preset);
}