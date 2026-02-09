using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Infrastructure.Models;

namespace ThePizzaDatabaseAPI.Infrastructure;

public class MongoPizzaRepository : IPizzaRepository
{
    private readonly IMongoCollection<PizzaDocument> _pizzasCollection;
    private readonly IWeeklyShuffler _weeklyShuffler;

    public MongoPizzaRepository(IMongoClient mongoClient, IWeeklyShuffler weeklyShuffler)
    {
        var db = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));

        _pizzasCollection = db.GetCollection<PizzaDocument>("pizzas");
        _weeklyShuffler = weeklyShuffler;
    }

    public async Task<List<Pizza>> GetAllAsync(string lang)
    {
        var allPizzas = await _pizzasCollection.Find(_ => true).ToListAsync();
        var mappedPizzas = MapToPizzaList(allPizzas, lang);

        return _weeklyShuffler.Shuffle(mappedPizzas);
    }

    public async Task<List<Pizza>> GetVegPizzasAsync(string lang)
    {
        var filter = Builders<PizzaDocument>.Filter.Eq(pizza => pizza.IsVegetarian, true);

        var vegPizzas = await _pizzasCollection.Find(filter).ToListAsync();
        var mappedPizzas = MapToPizzaList(vegPizzas, lang);

        return _weeklyShuffler.Shuffle(mappedPizzas);
    }

    public async Task<List<Pizza>> GetStuffedCrustPizzasAsync(string lang)
    {
        var filter = Builders<PizzaDocument>.Filter.Eq(pizza => pizza.IsStuffCrust, true);

        var stuffCrustPizzas = await _pizzasCollection.Find(filter).ToListAsync();
        var mappedPizzas = MapToPizzaList(stuffCrustPizzas, lang);

        return _weeklyShuffler.Shuffle(mappedPizzas);
    }

    private List<Pizza> MapToPizzaList(List<PizzaDocument> allPizzas, string lang)
    {
        var pizzas = allPizzas.Select(pizza =>
        {
            var translation = lang.ToLower() == "it" ? pizza.Translations.It : pizza.Translations.En;

            return new Pizza
            {
                Id = pizza.Id,
                Name = translation.Name,
                Ingredients = translation.Ingredients,
                Note = translation.Note,
                Image = pizza.Image,
                IsVegetarian = pizza.IsVegetarian,
                IsStuffCrust = pizza.IsStuffCrust
            };
        }).ToList();

        return pizzas;
    }
}