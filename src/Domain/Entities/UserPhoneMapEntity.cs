using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities;

public class UserPhoneMapEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk(Phone);

    [JsonPropertyName("sk")] public string Sk => UserId;
    [JsonPropertyName("phone")] public string Phone { get; set; } = default!;
    [JsonPropertyName("userId")] public string UserId { get; set; } = default!;

    public static string GetPk(string pkKey)
    {
        return $"UserMapping#{pkKey}";
    }
}