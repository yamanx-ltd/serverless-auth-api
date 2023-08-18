using Domain.Domains;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<MessageDto?> GetMessageAsync(string culture, string key, CancellationToken cancellationToken = default)
    {
        var message = await _messageRepository.GetMessageAsync(culture, key, cancellationToken);
        if (message == null)
            return null;

        return new MessageDto
        {
            Message = message.Message,
            Title = message.Title
        };
    }
}