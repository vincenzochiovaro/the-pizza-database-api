namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IPresetRepository
{
    Task<List<string>> GetStepsByPresetAndLang(string presetTitle, string lang);
}