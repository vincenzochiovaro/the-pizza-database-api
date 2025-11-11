

using MongoDB.Bson.Serialization.Conventions;

namespace ThePizzaDatabaseAPI.Infrastructure.Utility;

public static class MongoConventions
{
    public static void RegisterConventions()
    {
        var conventionPack = new ConventionPack()
        {
            new CamelCaseElementNameConvention()
        };
        ConventionRegistry.Register("Conventions", conventionPack, type => true);
    }
}