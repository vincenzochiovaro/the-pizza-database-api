using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Models.Messages;

public class ReminderScheduleMessage
{
    public required string Email { get; set; }

    public required ReminderSchedule Reminders { get; set; }
}