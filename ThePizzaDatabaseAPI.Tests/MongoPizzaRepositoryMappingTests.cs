using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Infrastructure.Models;

namespace ThePizzaDatabaseAPI.Tests;

public class MongoPizzaRepositoryMappingTests
{
    [Fact]
    public void GivenPizzaDocuments_WhenLangIsIt_ReturnsItalianFields()
    {
        // Given
        var langUnderTest = "it";
        var pizzaDocs = new List<PizzaDocument>
        {
            new PizzaDocument
            {
                Id = "1",
                Image = "img.png",
                IsVegetarian = true,
                Translations = new PizzaTranslations
                {
                    En = new PizzaTranslation
                    {
                        Name = "Test Pizza",
                        Ingredients = new List<string> { "Cheese", "Tomato" },
                        Note = "English note"
                    },
                    It = new PizzaTranslation
                    {
                        Name = "Pizza di Test",
                        Ingredients = new List<string> { "Formaggio", "Pomodoro" },
                        Note = "Nota italiana"
                    }
                }
            }
        };

        // When
        var pizzas = MapToPizzaListForTest(pizzaDocs, langUnderTest);

        // Then
        var pizza = pizzas.First();
        Assert.Equal("Pizza di Test", pizza.Name);
        Assert.Contains("Pomodoro", pizza.Ingredients);
        Assert.Equal("Nota italiana", pizza.Note);
        Assert.Equal("img.png", pizza.Image);
        Assert.True(pizza.IsVegetarian);
    }

    [Fact]
    public void GivenPizzaDocuments_WhenLangIsEn_ReturnsEnglishFields()
    {
        // Given
        var langUnderTest = "en";
        var pizzaDocs = new List<PizzaDocument>
        {
            new PizzaDocument
            {
                Id = "1",
                Image = "img.png",
                IsVegetarian = true,
                Translations = new PizzaTranslations
                {
                    En = new PizzaTranslation
                    {
                        Name = "Test Pizza",
                        Ingredients = new List<string> { "Cheese", "Tomato" },
                        Note = "English note"
                    },
                    It = new PizzaTranslation
                    {
                        Name = "Pizza di Test",
                        Ingredients = new List<string> { "Formaggio", "Pomodoro" },
                        Note = "Nota italiana"
                    }
                }
            }
        };

        // When
        var pizzas = MapToPizzaListForTest(pizzaDocs, langUnderTest);

        // Then
        var pizza = pizzas.First();
        Assert.Equal("Test Pizza", pizza.Name);
        Assert.Contains("Tomato", pizza.Ingredients);
        Assert.Equal("English note", pizza.Note);
        Assert.Equal("img.png", pizza.Image);
        Assert.True(pizza.IsVegetarian);
    }
    
    private List<Pizza> MapToPizzaListForTest(List<PizzaDocument> pizzaDocs, string lang)
    {
        return pizzaDocs.Select(pizza =>
        {
            var translation = lang.ToLower() == "it" ? pizza.Translations.It : pizza.Translations.En;

            return new Pizza
            {
                Id = pizza.Id,
                Name = translation.Name,
                Ingredients = translation.Ingredients,
                Note = translation.Note,
                Image = pizza.Image,
                IsVegetarian = pizza.IsVegetarian
            };
        }).ToList();
    }
}