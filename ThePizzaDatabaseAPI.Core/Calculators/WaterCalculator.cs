namespace ThePizzaDatabaseAPI.Core.Calculators;

public static class WaterCalculator
{
    public static int Calculate(double flour, int hydration)
    {
        return (int)Math.Round(flour * (hydration / 100.0));
    }
}