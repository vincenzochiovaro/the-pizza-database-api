namespace ThePizzaDatabaseAPI.Models.Requests;

public class GetPresetDataRequest
{
    public required string Preset { get; set; }
    public required string Lang { get; set; }
    public required int DoughBallCount { get; set; }
    public required int DoughBallWeight { get; set; }
    public required int Hydration { get; set; }
    public required int Temperature { get; set; }
    public int? Preferment { get; set; }
}