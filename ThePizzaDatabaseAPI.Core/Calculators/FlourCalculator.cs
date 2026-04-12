namespace ThePizzaDatabaseAPI.Core.Calculators;

public static class FlourCalculator
{
    public static int Calculate(string pizzaType, int doughBallCount, int doughBallWeight, int hydration)
    {
        if (pizzaType == "Biga")
        {
            return 1; // todo calc here
        }

        var totalDough = doughBallCount * doughBallWeight;

        return (int)(totalDough / (1 + (hydration / 100.0) + 0.025));
    }
}