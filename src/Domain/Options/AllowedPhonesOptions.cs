namespace Domain.Options;

public class AllowedPhonesOptions
{
    public bool AllowAll { get; set; }
    public List<string> Phones { get; set; } = new();
    public string Code { get; set; } = default!;
}