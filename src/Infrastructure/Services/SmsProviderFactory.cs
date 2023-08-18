using Domain.Services;

namespace Infrastructure.Services;

public class SmsProviderFactory : ISmsProviderFactory
{
    private readonly IEnumerable<ISmsProvider> _otpProviders;

    public SmsProviderFactory(IEnumerable<ISmsProvider> otpProviders)
    {
        _otpProviders = otpProviders;
    }

    private ISmsProvider Instance(string phoneNumber)
    {
        return _otpProviders.First();
    }

    public async Task<bool> SendSms(string phone, string message, CancellationToken cancellationToken)
    {
        var instance = Instance(phone);
        return await instance.SendSms(phone, message, cancellationToken);
    }
}