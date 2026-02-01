using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThePizzaDatabaseAPI.Infrastructure.Models;

public class PizzaDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public PizzaTranslations? Translations { get; set; }

    public string? Image { get; set; }

    public bool IsVegetarian { get; set; }
    public bool IsStuffCrust { get; set; }
    public bool IsClassic { get; set; }
}

public class PizzaTranslations
{
    public PizzaTranslation? En { get; set; }
    public PizzaTranslation? It { get; set; }
}

public class PizzaTranslation
{
    public string? Name { get; set; }
    public List<string>? Ingredients { get; set; } 
    public string? Note { get; set; }
}