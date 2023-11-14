using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Domain.Extensions;

namespace Domain.Entities;

public class OtpEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk(Key);

    [JsonPropertyName("sk")] public string Sk => Otp;
    [JsonPropertyName("phone")] public string Key { get; set; } = default!;

    [JsonPropertyName("userId")] public string? UserId { get; set; }
    [JsonPropertyName("otp")] public string Otp { get; set; } = default!;

    [JsonPropertyName("ttl")] public long Ttl => DateTime.UtcNow.AddMinutes(5).ToUnixTimeSeconds();

    public static string GetPk(string pkKey)
    {
        return $"LoginOtp#{pkKey}";
    }
}