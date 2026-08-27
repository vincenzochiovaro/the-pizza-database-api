namespace ThePizzaDatabaseAPI.Core.Domains;

public class PresetDoughIngredients
{ 
    public int Water { get; set; }
    public int Flour { get; set; }
    public double? Salt  { get; set; }
    public double Yeast { get; set; }
    public int? WaterDay2 { get; set; }
    public int? FlourDay2 { get; set; }
    public int? SaltDay2  { get; set; }
    public List<string> Steps { get; set; } = new();
    public required CookingTipsDomain TipsDomain { get; set; }
}