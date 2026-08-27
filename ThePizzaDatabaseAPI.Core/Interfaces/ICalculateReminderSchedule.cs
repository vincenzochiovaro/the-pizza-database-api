using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface ICalculateReminderSchedule
{
    ReminderScheduleDomain CalculateTimings(string date, string time, PizzaPreset preset);
}