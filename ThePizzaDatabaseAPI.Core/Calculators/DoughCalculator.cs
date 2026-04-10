namespace ThePizzaDatabaseAPI.Core.Calculators;

public static class DoughCalculator
{
    public static DoughResult Calculate()
    {
        return new DoughResult()
        {
            Water = 0,
            Flour = 0,
            Salt = null,
            WaterDay2 = 0,
            FlourDay2 = 0,
            SaltDay2 = 0,
            Yeast = 0,
        };
    }
}

public class DoughResult
{
    public int Water { get; set; }
    public int Flour { get; set; }
    public int? Salt  { get; set; }
    public int Yeast { get; set; }
    public int? WaterDay2 { get; set; }
    public int? FlourDay2 { get; set; }
    public int? SaltDay2  { get; set; }
}