using System.Collections.Generic;

namespace AdventureTimeApi.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a character in the Adventure Time API.
/// </summary>
public record CharacterDTO
{
    /// <summary>
    /// Gets the name of the character.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the gender of the character.
    /// </summary>
    public string Gender { get; init; }

    /// <summary>
    /// Gets the list of species associated with the character.
    /// </summary>
    public List<string> Species { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterDTO"/> record.
    /// </summary>
    /// <param name="name">The name of the character.</param>
    /// <param name="gender">The gender of the character.</param>
    /// <param name="species">The list of species associated with the character.</param>
    public CharacterDTO(string name, string gender, List<string> species)
    {
        Name = name;
        Gender = gender;
        Species = species;
    }
}
