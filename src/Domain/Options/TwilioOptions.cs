namespace Domain.Options;

public class TwilioOptions
{
    public bool IsEnabled { get; set; }
    public string AccountSid { get; set; } = default!;
    public string AuthToken { get; set; } = default!;
    public string From { get; set; } = default!;
}