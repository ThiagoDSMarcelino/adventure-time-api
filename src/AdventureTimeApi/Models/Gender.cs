using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models;

public record Gender()
{
    [JsonPropertyName("id")]
    public uint Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}