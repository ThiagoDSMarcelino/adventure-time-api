using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models.Genders;

public record Gender() : Model
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}