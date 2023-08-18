using Domain.Entities;

namespace Domain.Repositories;

public interface IMessageRepository
{
    Task<MessagesEntity?> GetMessageAsync(string culture, string key, CancellationToken cancellationToken = default);

    Task<bool> SaveMessageAsync(MessagesEntity entity, CancellationToken cancellationToken = default);
}