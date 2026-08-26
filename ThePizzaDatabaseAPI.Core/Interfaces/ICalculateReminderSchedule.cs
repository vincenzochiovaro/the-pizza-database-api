using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface ICalculateReminderSchedule
{
    ReminderSchedule CalculateTimings(string date, string time, PizzaPreset preset);
}