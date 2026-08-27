using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;

namespace ThePizzaDatabaseAPI.Models.Messages;

public class SendEmailMessage
{
    public required string RecipientEmail { get; init; }
    // todo need language
    public PizzaPreset Preset { get; init; }
    public required ReminderSchedule Reminders { get; init; }
    public required ReminderRound Round { get; init; }
}