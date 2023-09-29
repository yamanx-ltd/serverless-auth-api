namespace Domain.Options;

public class NetGsmOptions
{
    public bool IsEnabled { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string From { get; set; } = default!;
}