using ThePizzaDatabaseAPI.Core.Enums;

namespace ThePizzaDatabaseAPI.Models.Requests;

public class TimerSubmitScheduleRequest
{
    public required string Date { get; set; }

    public required string Time { get; set; }

    public required string Email { get; set; }

    public required PizzaPreset Preset { get; set; }

    public required DoughIngredients PresetData { get; set; }

    public required string Lang { get; set; }
}

public class DoughIngredients
{
    public int Water { get; set; }
    public int Flour { get; set; }
    public int Salt { get; set; }
    public double Yeast { get; set; }

    public int? WaterDay2 { get; set; }
    public int? FlourDay2 { get; set; }
    public int? SaltDay2 { get; set; }

    public required List<string> Steps { get; set; }
    public required CookingTips Tips { get; set; }
}

public class CookingTips
{
    public required List<string> Home { get; set; }
    public required List<string> Professional { get; set; }
}