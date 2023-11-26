using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models;

/// <summary>
/// Represents a gender in the Adventure Time API.
/// </summary>
public record Gender
{
    /// <summary>
    /// Gets the unique identifier of the gender.
    /// </summary>
    [JsonPropertyName("id")]
    public uint Id { get; init; }

    /// <summary>
    /// Gets the name of the gender.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
