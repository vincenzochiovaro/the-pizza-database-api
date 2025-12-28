using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThePizzaDatabaseAPI.Core.Contracts
{
    public class Pizza
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public required string Name { get; set; }
        public required int PrepTime { get; set; }
        public required int Price { get; set; }
        public string? Image { get; set; }
        public required int Temp { get; set; }
        public required string Style { get; set; }
        public required int Views { get; set; }
        public required string Oven { get; set; }
    }
}