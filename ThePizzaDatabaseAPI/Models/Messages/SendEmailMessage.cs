using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Models.Messages;

public class SendEmailMessage
{
    public required string RecipientEmail { get; init; }
    public required string Preset {get; init; }
    public required ReminderSchedule Reminders { get; init; }
    public required ReminderRound Round { get; init; }
}