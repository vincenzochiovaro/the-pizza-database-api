using MongoDB.Driver;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Domains;
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

    public async Task<List<PizzaDomain>> GetAllAsync(string lang)
    {
        var allPizzas = await _pizzasCollection.Find(_ => true).ToListAsync();
        var mappedPizzas = MapToPizzaList(allPizzas, lang);

        return _weeklyShuffler.Shuffle(mappedPizzas);
    }

    public async Task<List<PizzaDomain>> GetVegPizzasAsync(string lang)
    {
        var filter = Builders<PizzaDocument>.Filter.Eq(pizza => pizza.IsVegetarian, true);

        var vegPizzas = await _pizzasCollection.Find(filter).ToListAsync();
        var mappedPizzas = MapToPizzaList(vegPizzas, lang);

        return _weeklyShuffler.Shuffle(mappedPizzas);
    }

    public async Task<List<PizzaDomain>> GetWhitePizzasAsync(string lang)
    {
        var filter = Builders<PizzaDocument>.Filter.Eq(pizza => pizza.IsWhite, true);

        var whitePizzas = await _pizzasCollection.Find(filter).ToListAsync();
        var mappedPizzas = MapToPizzaList(whitePizzas, lang);

        return _weeklyShuffler.Shuffle(mappedPizzas);
    }

    private List<PizzaDomain> MapToPizzaList(List<PizzaDocument> allPizzas, string lang)
    {
        var pizzas = allPizzas.Select(pizza =>
        {
            var translation = lang.ToLower() == "it" ? pizza.Translations.It : pizza.Translations.En;

            return new PizzaDomain
            {
                Id = pizza.Id,
                Name = translation.Name,
                Ingredients = translation.Ingredients,
                Note = translation.Note,
                Image = pizza.Image,
                IsVegetarian = pizza.IsVegetarian,
                IsWhite = pizza.IsWhite
            };
        }).ToList();

        return pizzas;
    }
}