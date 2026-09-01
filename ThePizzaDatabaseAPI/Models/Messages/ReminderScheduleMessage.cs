using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;
using ThePizzaDatabaseAPI.Models.Requests;

namespace ThePizzaDatabaseAPI.Models.Messages;

public class ReminderScheduleMessage
{
    public required string Email { get; set; }
    
    public required Language Lang { get; set; }

    public required PizzaPreset  Preset { get; set; }
    public required MixingType MixingType { get; set; }
    
    public required DoughIngredients  PresetData { get; set; }
    public required ReminderScheduleDomain Reminders { get; set; }
}