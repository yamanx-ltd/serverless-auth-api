using Domain.Services;

namespace Infrastructure.Services.Providers;

public class MockSmsProvider : ISmsProvider
{
    public Task<bool> SendSms(string phone, string message, CancellationToken cancellationToken)
    {
        return Task.FromResult<bool>(true);
    }
}