namespace ThePizzaDatabaseAPI.Models.Requests;

public class GetPresetDataRequest
{
    public required string Preset { get; set; }
    public required string Lang { get; set; }
    public int DoughBallCount { get; set; }
    public int DoughBallWeight { get; set; }
}