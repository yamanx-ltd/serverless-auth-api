using System.Text.Json.Serialization;

namespace Domain.Domains.Google.Captcha;

public class GoogleCaptchaResponseModel
{
    [JsonPropertyName("success")] public bool Success { get; set; }
    [JsonPropertyName("challenge_ts")] public DateTime ChallengeTs { get; set; }
    [JsonPropertyName("hostname")] public string Hostname { get; set; } = default!;
    [JsonPropertyName("error-codes")] public string[] ErrorCodes { get; set; } = default!;
}