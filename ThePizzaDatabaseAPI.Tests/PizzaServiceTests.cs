using AutoFixture;
using Moq;
using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Core.Enums;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Services;

namespace ThePizzaDatabaseAPI.Tests;

public class PizzaServiceTests
{
    private readonly Mock<IPizzaRepository> _repository = new();
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task GivenFilterIsUnknown_WhenGettingPizzas_Then_Default_AllPizzasAreReturned()
    {
        // Given
        var filter =  (PizzaFilter)999;
        var lang = "any";
        var dummyPizzas = _fixture.Build<Pizza>()
            .CreateMany(2)
            .ToList();

        _repository.Setup(pizza => pizza.GetAllAsync(lang)).ReturnsAsync(dummyPizzas);

        // When
        var sut = new PizzaService(_repository.Object);

        // Then
        await sut.GetPizzasByFilter(filter, lang);

        _repository.Verify(pizza => pizza.GetAllAsync(lang), Times.Once);
    }

    [Fact]
    public async Task GivenFilterIsAllPizzas_WhenGettingPizzas_ThenAllPizzasAreReturned()
    {
        // Given
        var filter = PizzaFilter.AllPizzas;
        var lang = "any";
        var dummyPizzas = _fixture.Build<Pizza>()
            .CreateMany(2)
            .ToList();

        _repository.Setup(pizza => pizza.GetAllAsync(lang)).ReturnsAsync(dummyPizzas);

        // When
        var sut = new PizzaService(_repository.Object);

        // Then
        await sut.GetPizzasByFilter(filter, lang);

        _repository.Verify(pizza => pizza.GetAllAsync(lang), Times.Once);
    }

    [Fact]
    public async Task GivenFilterIsVegetarianPizzas_WhenGettingPizzas_TheOnlyVegetarianPizzasAreReturned()
    {
        // Given
        var filter = PizzaFilter.VegetarianPizzas;
        var lang = "any";
        var dummyPizzas = _fixture.Build<Pizza>()
            .CreateMany(3)
            .ToList();
        
        dummyPizzas[0].IsVegetarian = true;
        dummyPizzas[1].IsVegetarian = true;
        dummyPizzas[2].IsVegetarian = false;
        
        var vegetarianOnly = dummyPizzas.Where(p => p.IsVegetarian).ToList();

        _repository.Setup(pizza => pizza.GetVegPizzasAsync(lang)).ReturnsAsync(vegetarianOnly);
        
        // When
        var sut = new PizzaService(_repository.Object);
        
        var result = await sut.GetPizzasByFilter(filter, lang);
        
        // Then
        _repository.Verify(pizza => pizza.GetVegPizzasAsync(lang), Times.Once);
        
        var vegetarianPizzas = result.Count(x => x.IsVegetarian);
        Assert.Equal(2, vegetarianPizzas);

        Assert.NotEmpty(result);
        Assert.All(result, p => Assert.True(p.IsVegetarian));
    }
    
    [Fact]
    public async Task GivenFilterIsStuffedCrustPizzas_WhenGettingPizzas_ThenOnlyStuffedCrustPizzasAreReturned()
    {
        // Given
        var filter = PizzaFilter.StuffedCrustPizzas;
        var lang = "any";
        var dummyPizzas = _fixture.Build<Pizza>()
            .CreateMany(3)
            .ToList();

        dummyPizzas[0].IsStuffCrust = true;
        dummyPizzas[1].IsStuffCrust = true;
        dummyPizzas[2].IsStuffCrust = false;

        var stuffedCrustOnly = dummyPizzas.Where(pizza => pizza.IsStuffCrust).ToList();

        _repository.Setup(pizza => pizza.GetStuffedCrustPizzasAsync(lang)).ReturnsAsync(stuffedCrustOnly);
        
        // When
        var sut = new PizzaService(_repository.Object);
        
        var result = await sut.GetPizzasByFilter(filter, lang);

        // Then
        _repository.Verify(pizza => pizza.GetStuffedCrustPizzasAsync(lang), Times.Once);

        var stuffedCrustCount = result.Count(pizza => pizza.IsStuffCrust);
        Assert.Equal(2, stuffedCrustCount);
        
        Assert.NotEmpty(result);
        Assert.All(result, pizza => Assert.True(pizza.IsStuffCrust));
    }

    [Fact]
    public async Task GivenFilterIsClassicPizzas_WhenGettingPizzas_ThenOnlyClassicPizzasAreReturned()
    {
        // TODO: Implement the filtering logic and complete this test in a future task.
        // This test will verify that when the ClassicPizzas filter is used,
        // the service returns only pizzas that are considered classic.
    }
}