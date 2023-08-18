using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities;

public class UserPasswordMapEntity: IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk(UserId);

    [JsonPropertyName("sk")] public string Sk => Password;

    [JsonPropertyName("password")] public string Password { get; set; } = default!;
    [JsonPropertyName("userId")] public string UserId { get; set; } = default!;

    public static string GetPk(string pkKey)
    {
        return $"UserMapping#{pkKey}#Password";
    }
}