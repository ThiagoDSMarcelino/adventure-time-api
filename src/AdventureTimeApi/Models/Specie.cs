using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models;

/// <summary>
/// Represents a species in the Adventure Time API.
/// </summary>
public record Specie
{
    /// <summary>
    /// Gets the unique identifier of the species.
    /// </summary>
    [JsonPropertyName("id")]
    public uint Id { get; init; }

    /// <summary>
    /// Gets the name of the species.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
