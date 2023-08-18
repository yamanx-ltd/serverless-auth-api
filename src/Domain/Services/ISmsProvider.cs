namespace Domain.Services;

public interface ISmsProvider
{
    Task<bool> SendSms(string phone, string message, CancellationToken cancellationToken);
}