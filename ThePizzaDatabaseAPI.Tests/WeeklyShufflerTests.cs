using AutoFixture;
using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Core.Services;

namespace ThePizzaDatabaseAPI.Tests;

public class WeeklyShufflerTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void GivenSameWeek_WhenShuffleTwice_ThenOrderIsTheSame()
    {
        // Given
        var pizzas = _fixture.Create<List<Pizza>>();
        var fixedWeek = new DateTime(2026, 2, 9); // week 7

        var sut = new WeeklyShuffler(() => fixedWeek);

        // When
        var firstResult = sut.Shuffle(pizzas);
        var secondResult = sut.Shuffle(pizzas);

        // Then
        Assert.Equal(firstResult, secondResult);
    }

    [Fact]
    public void GivenDifferentWeeks_WhenShuffle_ThenOrderIsDifferent()
    {
        // Given
        var pizzas = _fixture.Create<List<Pizza>>();

        var weekOne = new WeeklyShuffler(() => new DateTime(2026, 2, 9));  // week 7
        var weekTwo = new WeeklyShuffler(() => new DateTime(2026, 2, 16)); // week 8

        // When
        var firstResult = weekOne.Shuffle(pizzas);
        var secondResult = weekTwo.Shuffle(pizzas);

        // Then
        Assert.NotEqual(firstResult, secondResult);
    }

    [Fact]
    public void GivenAnyWeek_WhenShuffle_ThenAllPizzasArePreserved()
    {
        // Given
        var pizzas = _fixture.Create<List<Pizza>>();
        var sut = new WeeklyShuffler(() => new DateTime(2026, 2, 9));

        // When
        var result = sut.Shuffle(pizzas);

        // Then
        Assert.Equal(pizzas.Count, result.Count);
        Assert.True(pizzas.All(pizza => result.Contains(pizza)));
    }
}