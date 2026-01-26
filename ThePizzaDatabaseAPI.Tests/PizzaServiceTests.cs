using AutoFixture;
using MongoDB.Bson;
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
        var dummyPizzas = _fixture.Build<Pizza>()
            .With(pizza => pizza.Id, ObjectId.GenerateNewId())
            .CreateMany(2)
            .ToList();

        _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(dummyPizzas);

        // When
        var sut = new PizzaService(_repository.Object);

        // Then
        await sut.GetPizzasByFilter(filter);

        _repository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GivenFilterIsAllPizzas_WhenGettingPizzas_ThenAllPizzasAreReturned()
    {
        // Given
        var filter = PizzaFilter.AllPizzas;
        var dummyPizzas = _fixture.Build<Pizza>()
            .With(pizza => pizza.Id, ObjectId.GenerateNewId())
            .CreateMany(2)
            .ToList();

        _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(dummyPizzas);

        // When
        var sut = new PizzaService(_repository.Object);

        // Then
        await sut.GetPizzasByFilter(filter);

        _repository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GivenFilterIsVegetarianPizzas_WhenGettingPizzas_TheOnlyVegetarianPizzasAreReturned()
    {
        // Given
        var filter = PizzaFilter.VegetarianPizzas;
        var dummyPizzas = _fixture.Build<Pizza>()
            .With(pizza => pizza.Id, ObjectId.GenerateNewId())
            .CreateMany(3)
            .ToList();
        
        dummyPizzas[0].IsVegetarian = true;
        dummyPizzas[1].IsVegetarian = true;
        dummyPizzas[2].IsVegetarian = false;
        
        _repository.Setup(x => x.GetVegPizzasAsync()).ReturnsAsync(dummyPizzas);
        
        // When
        var sut = new PizzaService(_repository.Object);
        
        var result = await sut.GetPizzasByFilter(filter);
        
        // Then
        _repository.Verify(x => x.GetVegPizzasAsync(), Times.Once);
        
        var vegetarianPizzas = result.Count(x => x.IsVegetarian);
        Assert.Equal(2, vegetarianPizzas);
    }
}