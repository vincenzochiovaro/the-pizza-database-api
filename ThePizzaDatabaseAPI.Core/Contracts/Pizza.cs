using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThePizzaDatabaseAPI.Core.Contracts
{
    public class Pizza
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public required string Name { get; set; }
        public string? Image { get; set; }
        public string? Note { get; set; }
        public bool IsVegetarian { get; set; }
        public required List<string> Ingredients { get; set; }
    }
}