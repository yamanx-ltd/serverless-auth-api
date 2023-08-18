using Domain.Domains;

namespace Domain.Services;

public interface IMessageService
{
    Task<MessageDto?> GetMessageAsync(string culture, string key, CancellationToken cancellationToken = default);
}