using ThePizzaDatabaseAPI.Core.Enums;
using ThePizzaDatabaseAPI.Core.Services;

namespace ThePizzaDatabaseAPI.Tests;

public class CalculateReminderScheduleTests
{
    private readonly CalculateReminderSchedule _sut = new();

    [Fact]
    public void CalculateTimings_Direct_ReturnsThreeRoundsInChronologicalOrder()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Direct;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.True(result.FirstRoundTime < result.SecondRoundTime);
        Assert.True(result.SecondRoundTime < result.ThirdRoundTime);
    }

    [Fact]
    public void CalculateTimings_Direct_FirstRoundIs8HoursAnd10MinutesBeforeThirdRound()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Direct;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.Equal(
            result.ThirdRoundTime.AddHours(-8),
            result.FirstRoundTime);
    }

    [Fact]
    public void CalculateTimings_Direct_SecondRoundIs4HoursBeforeThirdRound()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Direct;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.Equal(
            result.ThirdRoundTime.AddHours(-4),
            result.SecondRoundTime);
    }

    [Fact]
    public void CalculateTimings_Biga_ReturnsThreeRoundsInChronologicalOrder()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Biga;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.True(result.FirstRoundTime < result.SecondRoundTime);
        Assert.True(result.SecondRoundTime < result.ThirdRoundTime);
    }

    [Fact]
    public void CalculateTimings_Biga_FirstRoundIs18HoursBeforeThirdRound()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Biga;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.Equal(
            result.ThirdRoundTime.AddHours(-18),
            result.FirstRoundTime);
    }

    [Fact]
    public void CalculateTimings_Biga_SecondRoundIsAfterFirstRound()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Biga;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.True(result.SecondRoundTime > result.FirstRoundTime);
    }

    [Fact]
    public void CalculateTimings_Express_ReturnsThreeRoundsInChronologicalOrder()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Express;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.True(result.FirstRoundTime < result.SecondRoundTime);
        Assert.True(result.SecondRoundTime < result.ThirdRoundTime);
    }

    [Fact]
    public void CalculateTimings_Express_FirstRoundIs3HoursBeforeThirdRound()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Express;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.Equal(
            result.ThirdRoundTime.AddHours(-3),
            result.FirstRoundTime);
    }

    [Fact]
    public void CalculateTimings_Express_SecondRoundIs2HoursBeforeThirdRound()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";
        const PizzaPreset preset = PizzaPreset.Express;
        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        Assert.Equal(result.ThirdRoundTime.AddHours(-2), result.SecondRoundTime);
    }

    [Fact]
    public void CalculateTimings_ThirdRoundMatchesSelectedDateAndTime()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "20:00";

        // Act
        var directResult = _sut.CalculateTimings(date, time, PizzaPreset.Direct);
        var bigaResult = _sut.CalculateTimings(date, time, PizzaPreset.Biga);
        var expressResult = _sut.CalculateTimings(date, time, PizzaPreset.Express);

        // Assert
        var expected = new DateTime(2026, 8, 27, 20, 0, 0);

        Assert.Equal(expected, directResult.ThirdRoundTime);
        Assert.Equal(expected, bigaResult.ThirdRoundTime);
        Assert.Equal(expected, expressResult.ThirdRoundTime);
    }
    
    [Fact]
    public void CalculateTimings_Direct_WhenSelectedTimeIsAt01_ReturnsFirstRoundOnPreviousDay()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "01:00";
        const PizzaPreset preset = PizzaPreset.Direct;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        var expected = new DateTime(2026, 8, 26, 17, 00, 0);

        Assert.Equal(expected, result.FirstRoundTime);
    }

    [Fact]
    public void CalculateTimings_Biga_WhenSelectedTimeIsAt01_ReturnsFirstRoundOnPreviousDay()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "01:00";
        const PizzaPreset preset = PizzaPreset.Biga;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        var expected = new DateTime(2026, 8, 26, 07, 00, 0);

        Assert.Equal(expected, result.FirstRoundTime);
    }

    [Fact]
    public void CalculateTimings_Express_WhenSelectedTimeIsAt01_ReturnsFirstRoundOnPreviousDay()
    {
        // Arrange
        const string date = "2026-08-27";
        const string time = "01:00";
        const PizzaPreset preset = PizzaPreset.Express;

        // Act
        var result = _sut.CalculateTimings(date, time, preset);

        // Assert
        var expected = new DateTime(2026, 8, 26, 22, 00, 0);

        Assert.Equal(expected, result.FirstRoundTime);
    }
}