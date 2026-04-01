using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

public class PresetsDataService
{
    private readonly IPresetRepository _presetRepository;

    public PresetsDataService(IPresetRepository presetRepository)
    {
        _presetRepository = presetRepository;
    }

    public async Task<PresetDoughIngredients> GetPresetDataAsync(
        string presetTitle,
        string lang,
        int doughBallCount,
        int doughBallWeight)
    {

        var steps = await _presetRepository.GetStepsByPresetAndLang(presetTitle, lang);
        var tips = "tips"; // todo call repository to retrieve tips
        
        // TODO: call DoughBuilderCalculator() to calculate ingredient quantities
        // based on doughBallCount and doughBallWeight

        return new PresetDoughIngredients
        {
            Water = 0,
            Flour = 0,
            Salt = 0,
            Yeast = 0,
            Steps = steps,
            Tips = tips
        };
    }
}