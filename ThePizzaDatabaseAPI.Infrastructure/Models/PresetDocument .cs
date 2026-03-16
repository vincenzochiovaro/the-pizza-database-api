using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThePizzaDatabaseAPI.Infrastructure.Models;

public class PresetDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

}