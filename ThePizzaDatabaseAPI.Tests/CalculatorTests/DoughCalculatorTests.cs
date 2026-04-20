using ThePizzaDatabaseAPI.Core.Calculators;

namespace ThePizzaDatabaseAPI.Tests.CalculatorTests;

public class DoughCalculatorTests
{
    [Fact]
    public void when_type_is_biga_then_salt_day1_is_zero_and_day2_has_value()
    {
        // Given
        var result = DoughCalculator.Calculate("Biga", 1, 1000, 60, 20, 50);

        // When / Then
        Assert.Equal(0, result.Salt);
        Assert.True(result.SaltDay2 > 0);
    }
    
    [Fact]
    public void when_not_biga_then_all_values_are_in_day1()
    {
        // Given
        var result = DoughCalculator.Calculate("Direct", 1, 1000, 60, 20, null);

        // Then
        Assert.Null(result.WaterDay2);
        Assert.Null(result.FlourDay2);
        Assert.Null(result.SaltDay2);
    }
    
    [Fact]
    public void when_biga_then_flour_is_split_between_day1_and_day2()
    {
        // Given
        var result = DoughCalculator.Calculate("Biga", 1, 1000, 60, 20, 50);

        // Then
        Assert.True(result.Flour > 0);
        Assert.True(result.FlourDay2 > 0);
        Assert.Equal(result.Flour + result.FlourDay2, 
            FlourCalculator.Calculate(1000, 60));
    }
    
    [Fact]
    public void when_biga_then_day1_water_uses_biga_hydration()
    {
        // Given
        var result = DoughCalculator.Calculate("Biga", 1, 1000, 60, 20, 50);

        // BIGA_HYDRATION = 48%
        var expectedWaterDay1 = (int)(result.Flour * 0.48);

        // Then
        Assert.Equal(expectedWaterDay1, result.Water);
    }
    
    [Fact]
    public void when_biga_then_total_water_is_preserved()
    {
        // Given
        var result = DoughCalculator.Calculate("Biga", 1, 1000, 60, 20, 50);

        var totalFlour = FlourCalculator.Calculate(1000, 60);
        var expectedTotalWater = WaterCalculator.Calculate(totalFlour, 60);

        // Then
        Assert.Equal(expectedTotalWater, result.Water + result.WaterDay2);
    }
    
    [Fact]
    public void yeast_should_change_based_on_pizza_type()
    {
        // Given
        var biga = DoughCalculator.Calculate("Biga", 1, 1000, 60, 20, 50);
        var direct = DoughCalculator.Calculate("Direct", 1, 1000, 60, 20, null);

        // Then
        Assert.NotEqual(biga.Yeast, direct.Yeast);
    }
}