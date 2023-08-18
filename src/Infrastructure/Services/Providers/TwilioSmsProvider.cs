using Domain.Options;
using Domain.Services;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Infrastructure.Services.Providers;

public class TwilioSmsProvider : ISmsProvider
{
    private readonly IOptionsSnapshot<TwilioOptions> _twilioOptionsSnapshot;

    public TwilioSmsProvider(IOptionsSnapshot<TwilioOptions> twilioOptionsSnapshot)
    {
        _twilioOptionsSnapshot = twilioOptionsSnapshot;
    }

    public async Task<bool> SendSms(string phone, string message, CancellationToken cancellationToken)
    {
        var userName = _twilioOptionsSnapshot.Value.AccountSid;
        var password = _twilioOptionsSnapshot.Value.AuthToken;
        var sender = _twilioOptionsSnapshot.Value.From;

        TwilioClient.Init(username: userName, password: password);

        var twilioMessage = await MessageResource.CreateAsync(
            body: message,
            from: new Twilio.Types.PhoneNumber(sender),
            to: new Twilio.Types.PhoneNumber(phone)
        );

        return twilioMessage.ErrorCode == null;
    }
}