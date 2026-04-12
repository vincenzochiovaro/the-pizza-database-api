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
        var flour = FlourCalculator.Calculate(pizzaType, doughBallCount, doughBallWeight, hydration);
        var yeast = YeastCalculator.Calculate(pizzaType, temperature, flour);
        var salt = SaltCalculator.Calculate(pizzaType, flour);
        var water = WaterCalculator.Calculate(pizzaType, flour, hydration);
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