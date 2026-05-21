using MongoDB.Bson.Serialization.Attributes;

namespace ThePizzaDatabaseAPI.Infrastructure.Models;

public class PresetDocument
{
    [BsonId] public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public StepLanguages Steps { get; set; } = new();

    public CookingTipLanguages CookingTips { get; set; } = new();
}

public class StepLanguages
{
    public List<string> En { get; set; } = new();
    public List<string> It { get; set; } = new();
}

public class CookingTipLanguages
{
    public CookingOvenTips It { get; set; } = new();
    public CookingOvenTips En { get; set; } = new();
}

public class CookingOvenTips
{
    public List<string> Home { get; set; }
    public List<string> Professional { get; set; }
}