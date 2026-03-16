using ThePizzaDatabaseAPI.Core.Contracts;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IPresetRepository
{
    Task<PresetData?> GetByPresetAsync(string preset);
}