namespace Domain.Options;

public class CaptchaOptions
{
    public GoogleOptions? Google { get; set; }
    public CloudflareOptions? Cloudflare { get; set; }

    public class GoogleOptions
    {
        public bool IsEnabled { get; set; }
        public string ValidationUrl { get; set; } = default!;
        public string ServerKey { get; set; } = default!;
    }

    public class CloudflareOptions
    {
        public bool IsEnabled { get; set; }

        public string ValidationUrl { get; set; } = default!;

        public string SecretKey { get; set; } = default!;
    }
}