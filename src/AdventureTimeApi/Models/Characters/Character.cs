using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models.Characters;

public record Character
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("gender_dk")]
    public int GenderFK { get; set; }
}