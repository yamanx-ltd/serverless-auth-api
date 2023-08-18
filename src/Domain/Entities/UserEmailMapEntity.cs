using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities;

public class UserEmailMapEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk(Email);

    [JsonPropertyName("sk")] public string Sk => UserId;

    [JsonPropertyName("email")] public string Email { get; set; } = default!;
    [JsonPropertyName("userId")] public string UserId { get; set; } = default!;

    public static string GetPk(string pkKey)
    {
        return $"UserMapping#{pkKey}";
    }
}