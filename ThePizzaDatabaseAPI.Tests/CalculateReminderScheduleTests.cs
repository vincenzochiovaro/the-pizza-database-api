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
            result.ThirdRoundTime.AddHours(-8).AddMinutes(-10),
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
        Assert.Equal(
            result.ThirdRoundTime.AddHours(-2),
            result.SecondRoundTime);
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
    public void CalculateTimings_Biga_WhenSelectedTimeIsLessThan20HoursFromNow_ThrowsException()
    {
        // Arrange
        var selectedDateTime = DateTime.UtcNow.AddHours(19);

        var date = selectedDateTime.ToString("yyyy-MM-dd");
        var time = selectedDateTime.ToString("HH:mm");

        // Act & Assert
        Assert.Throws<Exception>(() =>
            _sut.CalculateTimings(date, time, PizzaPreset.Biga));
    }

    [Fact]
    public void CalculateTimings_Direct_WhenSelectedTimeIsLessThanRequiredMinimumFromNow_ThrowsException()
    {
        // Arrange
        var selectedDateTime = DateTime.UtcNow.AddHours(8);

        var date = selectedDateTime.ToString("yyyy-MM-dd");
        var time = selectedDateTime.ToString("HH:mm");

        // Act & Assert
        Assert.Throws<Exception>(() =>
            _sut.CalculateTimings(date, time, PizzaPreset.Direct));
    }

    [Fact]
    public void CalculateTimings_Express_WhenSelectedTimeIsLessThanRequiredMinimumFromNow_ThrowsException()
    {
        // Arrange
        var selectedDateTime = DateTime.UtcNow.AddHours(2);

        var date = selectedDateTime.ToString("yyyy-MM-dd");
        var time = selectedDateTime.ToString("HH:mm");

        // Act & Assert
        Assert.Throws<Exception>(() =>
            _sut.CalculateTimings(date, time, PizzaPreset.Express));
    }
}