namespace ThePizzaDatabaseAPI.Core.Calculators;

public static class WaterCalculator
{
    public static int Calculate(string pizzaType, double flour, int hydration)
    {
        if (pizzaType == "Biga")
        {
            return 1; // todo calc
        }

        return (int)Math.Round(flour * (hydration / 100.0));
    }
}