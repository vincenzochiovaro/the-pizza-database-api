using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;

namespace ThePizzaDatabaseAPI.Models.Messages;

public class SendEmailMessage
{
    public required string RecipientEmail { get; init; }
    public required Language Lang { get; init; }
    public required PizzaPreset Preset { get; init; }
    public required MixingType MixingType { get; init; }
    public required ReminderScheduleDomain Reminders { get; init; }
    public required ReminderRound Round { get; init; }
}