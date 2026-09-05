using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

public class ReminderMessageService
{
    private readonly IReminderMessageRepository _repository;
    
    public ReminderMessageService(IReminderMessageRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ReminderMessageDomain> GetByPresetAsync(string preset)
    {
        return await _repository.GetByPresetAsync(preset);
    }
}