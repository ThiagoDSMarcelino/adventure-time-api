using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AdventureTimeApi.Models;

namespace AdventureTimeApi.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a character in the Adventure Time API.
/// </summary>
[JsonConverter(typeof(CharacterDTOConverter))]
public record CharacterDTO
{
    /// <summary>
    /// Gets the ID of the character.
    /// </summary>
    public uint Id { get; init; }

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
    /// Initializes a new instance of the <see cref="CharacterDTO"/> class based on the provided character and related data.
    /// </summary>
    /// <param name="character">The character model.</param>
    /// <param name="genders">The list of available genders.</param>
    /// <param name="charactersSpecies">The list of relationships between characters and species.</param>
    /// <param name="species">The list of available species.</param>
    public CharacterDTO(Character character, List<Gender> genders, List<CharacterSpecie> charactersSpecies, List<Specie> species)
    {
        var characterGender = genders.First(g => g.Id == character.GenderFK).Name;

        var characterSpecies = charactersSpecies
            .Where(cs => cs.CharacterFK == character.Id)
            .Select(cs => species.First(s => s.Id == cs.SpecieFK).Name)
            .ToList();

        Id = character.Id;
        Name = character.Name;
        Gender = characterGender;
        Species = characterSpecies;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterDTO"/> class directly with provided values.
    /// </summary>
    /// <param name="id">The ID of the character.</param>
    /// <param name="name">The name of the character.</param>
    /// <param name="gender">The gender of the character.</param>
    /// <param name="species">The list of species associated with the character.</param>
    public CharacterDTO(uint id, string name, string gender, List<string> species)
    {
        Id = id;
        Name = name;
        Gender = gender;
        Species = species;
    }
}
