using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models;

/// <summary>
/// Represents the relationship between characters and species in the Adventure Time API.
/// </summary>
public record CharacterSpecie
{
    /// <summary>
    /// Gets the unique identifier of the character-species relationship.
    /// </summary>
    [JsonPropertyName("id")]
    public uint Id { get; init; }

    /// <summary>
    /// Gets the foreign key to the character in the relationship.
    /// </summary>
    [JsonPropertyName("character_fk")]
    public uint CharacterFK { get; init; }

    /// <summary>
    /// Gets the foreign key to the species in the relationship.
    /// </summary>
    [JsonPropertyName("specie_fk")]
    public uint SpecieFK { get; init; }
}
