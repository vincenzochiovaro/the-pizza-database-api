using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThePizzaDatabaseAPI.Core.Models
{
    public class ContractPlaceHolder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
       public string PizzaName { get; set; }
    }
}