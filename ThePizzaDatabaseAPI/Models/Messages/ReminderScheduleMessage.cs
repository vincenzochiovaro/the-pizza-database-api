using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;

namespace ThePizzaDatabaseAPI.Models.Messages;

public class ReminderScheduleMessage
{
    public required string Email { get; set; }

    public PizzaPreset  Preset { get; set; }
    public required ReminderScheduleDomain Reminders { get; set; }
}