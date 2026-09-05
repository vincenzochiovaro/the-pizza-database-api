using ThePizzaDatabaseAPI.Core.Domains;

namespace ThePizzaDatabaseAPI.Core.Interfaces;

public interface IReminderMessageRepository
{
    Task<ReminderMessageDomain> GetByPresetAsync(string preset);
}