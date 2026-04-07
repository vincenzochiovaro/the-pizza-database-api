namespace ThePizzaDatabaseAPI.Models.Responses;

public class GetPresetResponse
{
    public int Water { get; set; }
    public int Flour { get; set; }
    public int Salt { get; set; }
    public int Yeast { get; set; }
    public List<string> Steps { get; set; } = new();
    
    public string? Tips { get; set; }
}