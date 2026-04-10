using ThePizzaDatabaseAPI.Core.Calculators;

namespace ThePizzaDatabaseAPI.Tests.CalculatorTests;

public class DoughCalculatorTests
{
    [Fact]
    public void when_type_is_biga_then_salt_on_day1_is_null()
    {
        var sut = DoughCalculator.Calculate("Direct", 0,0,0,0,0);
        
        Assert.Null(sut.Salt);
    }
}