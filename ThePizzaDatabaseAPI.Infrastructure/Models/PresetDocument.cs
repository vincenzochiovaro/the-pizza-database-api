using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThePizzaDatabaseAPI.Infrastructure.Models;

public class PresetDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Title { get; set; } = string.Empty;
    public StepLanguages Steps { get; set; } = new();
}

public class StepLanguages
{
    public List<string> En { get; set; } = new();
    public List<string> It { get; set; } = new();
}