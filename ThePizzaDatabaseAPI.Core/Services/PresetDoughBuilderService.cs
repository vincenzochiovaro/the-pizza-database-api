using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

public class PresetDoughBuilderService
{
    private readonly IPresetRepository _presetRepository;

    public PresetDoughBuilderService(IPresetRepository presetRepository)
    {
        _presetRepository = presetRepository;
    }

    public async Task<PresetDoughIngredients> GetDoughIngredients(
        string preset,
        string lang,
        int doughBallCount,
        int doughBallWeight)
    {
        // Repository call stubbed — real calculation to be implemented in a future ticket
        await _presetRepository.GetByPresetAsync(preset);

        // Mocked values 
        return new PresetDoughIngredients
        {
            Water = 360,
            Flour = 600,
            Salt = 12,
            Yeast = 3
        };
    }
}