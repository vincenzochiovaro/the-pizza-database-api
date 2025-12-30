using AutoFixture;
using MongoDB.Bson;
using Moq;
using ThePizzaDatabaseAPI.Core.Contracts;
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
        var filter = "invalidFilter";
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
        var filter = "All Pizzas";
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
    public async Task GivenFilterIsPreparationTime_WhenGettingPizzas_ThePizzasOrderedByPrepTimeIsReturned()
    {
        // TODO
    }
}