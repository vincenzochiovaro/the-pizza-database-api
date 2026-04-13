namespace ThePizzaDatabaseAPI.Core.Calculators;

public static class SaltCalculator
{
    public static double Calculate(double flour)
    {
        return Math.Round(flour * 0.025, 1);
    }
}