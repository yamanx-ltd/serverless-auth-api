using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities;

public class MessagesEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk(Culture);
    [JsonPropertyName("sk")] public string Sk => Type;
    [JsonPropertyName("type")] public string Type { get; set; } = default!;
    [JsonPropertyName("culture")] public string Culture { get; set; } = default!;
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("message")] public string Message { get; set; } = default!;

    public static string GetPk(string pkKey)
    {
        return $"Messages#{pkKey}";
    }
}