namespace ThePizzaDatabaseAPI.Core.Calculators;

public static class YeastCalculator
{
    public static double Calculate(string pizzaType, int temperature, double flour)
    {
        if (pizzaType == "Biga")
        {
            return 1;// todo calc here
        }

        var hours = pizzaType switch
        {
            "Direct" => 8,
            "Express" => 3,
            _ => 8
        };

        var tempFactor = temperature / 35.0;
        var inverted = 1 - tempFactor;
        var basePercent = 0.002 + (inverted * 0.008);
        var timeFactor = 8.0 / hours;

        var yeastPercent = basePercent * timeFactor;

        return Math.Round(flour * yeastPercent, 1);
    }
}