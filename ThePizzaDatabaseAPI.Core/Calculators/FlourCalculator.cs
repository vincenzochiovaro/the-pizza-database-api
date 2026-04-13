namespace ThePizzaDatabaseAPI.Core.Calculators;

public static class FlourCalculator
{
    public static int Calculate(int totalDough, int hydration)
    {
        var hydrationRatio = hydration / 100.0;

        return (int)(totalDough / (1 + hydrationRatio));
    }
}