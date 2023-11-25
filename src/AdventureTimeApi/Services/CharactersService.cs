using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using AdventureTimeApi.Config;
using AdventureTimeApi.Errors;
using AdventureTimeApi.Interfaces;
using AdventureTimeApi.Models.Characters;
using AdventureTimeApi.Models.Genders;

namespace AdventureTimeApi.Services;

public class CharactersService : ICharactersRepository
{
    public async Task<IEnumerable<CharacterDTO>> GetCharactersAsync(string? gender)
    {
        using FileStream charactersStream = File.OpenRead(Constants.CHARACTER_FILE_PATH);
        var characters = await JsonSerializer.DeserializeAsync<List<Character>>(charactersStream) ?? throw new LoadModelException(typeof(Character));

        using FileStream gendersStream = File.OpenRead(Constants.GENDER_FILE_PATH);
        var genders = await JsonSerializer.DeserializeAsync<List<Gender>>(gendersStream) ?? throw new LoadModelException(typeof(Gender));

        var data = characters.Select(c =>
        {
            var characterGender = genders.First(g => g.Id == c.GenderFK).Name;
            return CharacterDTO.Convert(c, characterGender);
        });

        if (gender is not null)
        {
            // Checks if the gender exists
            if (!genders.Any(g => string.Equals(g.Name, gender, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidGenderException(gender);

            return data.Where(c => string.Equals(c.Gender, gender, StringComparison.OrdinalIgnoreCase));
        }

        return data;
    }
}