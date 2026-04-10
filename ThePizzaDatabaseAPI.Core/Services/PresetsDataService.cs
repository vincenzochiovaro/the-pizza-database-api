using ThePizzaDatabaseAPI.Core.Calculators;
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
        int doughBallWeight,
        int hydration,
        int temperature,
        int? preferment)
    {
        var steps = await _presetRepository.GetStepsByPresetAndLang(presetTitle, lang);
        var tips = "tips"; // todo call repository to retrieve tips

        var doughIngredients = DoughCalculator.Calculate(
            presetTitle, doughBallCount, doughBallWeight, hydration, temperature, preferment);

        return new PresetDoughIngredients
        {
            Water = doughIngredients.Water,
            Flour = doughIngredients.Flour,
            Salt = doughIngredients.Salt,
            Yeast = doughIngredients.Yeast,
            WaterDay2 = doughIngredients.WaterDay2,
            FlourDay2 = doughIngredients.FlourDay2,
            SaltDay2 = doughIngredients.SaltDay2,
            Steps = steps,
            Tips = tips
        };
    }
}