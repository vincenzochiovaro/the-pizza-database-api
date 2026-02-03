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

        _repository.Setup(x => x.GetAllAsync(lang)).ReturnsAsync(dummyPizzas);

        // When
        var sut = new PizzaService(_repository.Object);

        // Then
        await sut.GetPizzasByFilter(filter, lang);

        _repository.Verify(x => x.GetAllAsync(lang), Times.Once);
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

        _repository.Setup(x => x.GetAllAsync(lang)).ReturnsAsync(dummyPizzas);

        // When
        var sut = new PizzaService(_repository.Object);

        // Then
        await sut.GetPizzasByFilter(filter, lang);

        _repository.Verify(x => x.GetAllAsync(lang), Times.Once);
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
        
        _repository.Setup(x => x.GetVegPizzasAsync(lang)).ReturnsAsync(dummyPizzas);
        
        // When
        var sut = new PizzaService(_repository.Object);
        
        var result = await sut.GetPizzasByFilter(filter, lang);
        
        // Then
        _repository.Verify(x => x.GetVegPizzasAsync(lang), Times.Once);
        
        var vegetarianPizzas = result.Count(x => x.IsVegetarian);
        Assert.Equal(2, vegetarianPizzas);
    }
    
    [Fact]
    public async Task GivenFilterIsStuffedCrustPizzas_WhenGettingPizzas_ThenOnlyStuffedCrustPizzasAreReturned()
    {
        // TODO: Implement the filtering logic and complete this test in a future task.
        // This test will verify that when the StuffedCrustPizzas filter is used,
        // the service returns only pizzas that have a stuffed crust.
    }

    [Fact]
    public async Task GivenFilterIsClassicPizzas_WhenGettingPizzas_ThenOnlyClassicPizzasAreReturned()
    {
        // TODO: Implement the filtering logic and complete this test in a future task.
        // This test will verify that when the ClassicPizzas filter is used,
        // the service returns only pizzas that are considered classic.
    }
}