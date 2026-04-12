namespace ThePizzaDatabaseAPI.Core.Calculators;

public static class SaltCalculator
{
    public static double Calculate(string pizzaType, double flour)
    {
        if (pizzaType == "Biga")
            return 0;

        return Math.Round(flour * 0.025, 1);
    }
}