using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models;

/// <summary>
/// Represents a character in the Adventure Time API.
/// </summary>
public record Character
{
    /// <summary>
    /// Gets the unique identifier of the character.
    /// </summary>
    [JsonPropertyName("id")]
    public uint Id { get; init; }

    /// <summary>
    /// Gets the name of the character.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the foreign key to the gender of the character.
    /// </summary>
    [JsonPropertyName("gender_fk")]
    public uint GenderFK { get; init; }
}
