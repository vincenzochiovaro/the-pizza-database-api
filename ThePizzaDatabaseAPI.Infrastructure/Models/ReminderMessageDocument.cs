using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThePizzaDatabaseAPI.Infrastructure.Models;

public class ReminderMessageDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; init; }

    public required string Preset { get; init; }
    public required ReminderRounds Rounds { get; init; }
}

public class ReminderRounds
{
    public required ReminderRoundTranslation Round1 { get; set; }
    public required ReminderRoundTranslation Round2 { get; set; }
    public required ReminderRoundTranslation Round3 { get; set; }
}

public class ReminderRoundTranslation
{
    public required ReminderMessageType En { get; set; }
    public required ReminderMessageType It { get; set; }
}

public class ReminderMessageType
{
    public required ReminderMessageTranslation Hands { get; set; }
    public required ReminderMessageTranslation Planetary { get; set; }
    public required ReminderMessageTranslation Spiral { get; set; }
}

public class ReminderMessageTranslation
{
    public required string Subject { get; set; }
    public required string Message { get; set; }
}