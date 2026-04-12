namespace ThePizzaDatabaseAPI.Core.Calculators;
// TODO REFACTOR ON HAVING FLOURCALCULATOR, WATER CALCULATOR ETC
public class DoughResult
{
    public int Water { get; set; }
    public int Flour { get; set; }
    public double? Salt { get; set; }
    public double Yeast { get; set; }
    public int? WaterDay2 { get; set; }
    public int? FlourDay2 { get; set; }
    public int? SaltDay2 { get; set; }
}

public static class DoughCalculator
{
    public static DoughResult Calculate(
        string pizzaType,
        int doughBallCount,
        int doughBallWeight,
        int hydration,
        int temperature,
        int? preferment)
    {
        var flour = CalcFlour(pizzaType, doughBallCount, doughBallWeight, hydration);
        var yeast = CalcYeast(pizzaType, temperature, flour);
        var salt = CalcSalt(pizzaType, flour);
        var water = CalcWater(pizzaType, flour, hydration);
        var flourDay2 = CalcFlourDayTwo();
        var waterDay2 = CalcWaterDayTwo();
        var saltDay2 = CalcSaltDayTwo();


        return new DoughResult()
        {
            Water = water,
            Flour = flour,
            Salt = salt,
            WaterDay2 = waterDay2,
            FlourDay2 = flourDay2,
            SaltDay2 = saltDay2,
            Yeast = yeast,
        };
    }

    private static double CalcYeast(string pizzaType, int temperature, double flour)
    {
        if (pizzaType == "Biga")
        {
            return 1;
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

    private static int CalcFlour(string pizzaType, int doughBallCount, int doughBallWeight, int hydration)
    {
        if (pizzaType == "Biga")
        {
            return 1; // todo calc here
        }

        var totalDough = doughBallCount * doughBallWeight;

        return (int)(totalDough / (1 + (hydration / 100.0) + 0.025));
    }

    private static double CalcSalt(string pizzaType, int flour)
    {
        if (pizzaType == "Biga")
            return 0;

        return Math.Round(flour * 0.025, 1);
    }

    private static int CalcWater(string pizzaType, int flour, int hydration)
    {
        if (pizzaType == "Biga")
        {
            return 1; // todo calc
        }

        return (int)Math.Round(flour * (hydration / 100.0));
    }

    private static int CalcFlourDayTwo()
    {
        return 1;
    }

    private static int CalcWaterDayTwo()
    {
        return 1;
    }

    private static int CalcSaltDayTwo()
    {
        return 1;
    }
}