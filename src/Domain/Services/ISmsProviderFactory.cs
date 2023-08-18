namespace Domain.Services;

public interface ISmsProviderFactory
{
    Task<bool> SendSms(string phone, string message, CancellationToken cancellationToken);
}