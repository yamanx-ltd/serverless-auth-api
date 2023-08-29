namespace Domain.Options;

public class AllowedPhonesOptions
{
    public List<string> Phones { get; set; } = new();
    public string Code { get; set; } = default!;
}