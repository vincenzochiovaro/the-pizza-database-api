using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IPresetRepository
{
    Task<List<string>> GetStepsByPresetAndLang(string presetTitle, string lang);

    Task<CookingTips> GetCookingTipsByLang(string presetTitle, string lang);
}