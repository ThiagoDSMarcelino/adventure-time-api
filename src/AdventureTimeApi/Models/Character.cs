using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models;

public record Character
{
    [JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("gender_fk")]
    public uint GenderFK { get; set; }
}