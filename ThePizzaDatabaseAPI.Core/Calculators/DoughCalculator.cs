namespace ThePizzaDatabaseAPI.Core.Calculators;

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
    private const int SALT_BIGA_DAY1 = 0;
    private const int BIGA_HYDRATION = 48;

    public static DoughResult Calculate(
        string pizzaType,
        int doughBallCount,
        int doughBallWeight,
        int hydration,
        int temperature,
        int? preferment)
    {
        var totalDough = doughBallCount * doughBallWeight;
        var totalFlour = FlourCalculator.Calculate(totalDough, hydration);
        var totalSalt = SaltCalculator.Calculate(totalFlour);
        var totalYeast = YeastCalculator.Calculate(pizzaType, temperature, totalFlour);
        var totalWater = WaterCalculator.Calculate(totalFlour, hydration);

        var (flourDay1, flourDay2) = Split(totalFlour, pizzaType == "Biga" ? preferment : null);
        var (saltDay1, saltDay2) = Split((int)totalSalt, pizzaType == "Biga" ? SALT_BIGA_DAY1 : null);
        var waterDay1 = pizzaType == "Biga" ? (int)(flourDay1 * (BIGA_HYDRATION / 100.0)) : totalWater;
        var waterDay2 = pizzaType == "Biga" ? totalWater - waterDay1 : (int?)null;

        return new DoughResult()
        {
            Water = waterDay1,
            Flour = flourDay1,
            Salt = saltDay1,
            WaterDay2 = waterDay2,
            FlourDay2 = flourDay2,
            SaltDay2 = saltDay2,
            Yeast = totalYeast,
        };
    }

    // This method splits a total value (flour, salt, etc.)
    // into two parts based on a percentage.
    // Example: total = 1000, percent = 80
    // result:
    // day1 = 800
    // day2 = 200
    private static (int day1, int? day2) Split(int total, int? percent)
    {
        // If there is no percent (not Biga), everything goes to day 1
        if (percent == null)
        {
            return (total, null);
        }

        var ratio = percent.Value / 100.0;

        var day1 = (int)(total * ratio);
        var day2 = total - day1;

        return (day1, day2);
    }
}