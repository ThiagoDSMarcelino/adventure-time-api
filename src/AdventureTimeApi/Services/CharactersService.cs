using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdventureTimeApi.Config;
using AdventureTimeApi.Interfaces;
using AdventureTimeApi.Models.Characters;
using AdventureTimeApi.Models.Genders;

namespace AdventureTimeApi.Services;

public class CharactersService : ICharactersRepository
{
    public async Task<IEnumerable<CharacterDTO>> GetCharactersAsync()
    {
        using FileStream charactersStream = File.OpenRead(Constants.CHARACTER_FILE_PATH);
        var characters = await JsonSerializer.DeserializeAsync<List<Character>>(charactersStream) ?? throw new Exception(); // TODO: Handle exception

        using FileStream gendersStream = File.OpenRead(Constants.GENDER_FILE_PATH);
        var genders = await JsonSerializer.DeserializeAsync<List<Gender>>(gendersStream) ?? throw new Exception(); // TODO: Handle exception

        var dto = characters.Select(c =>
        {
            var gender = genders.First(g => g.Id == c.GenderFK).Name;
            return CharacterDTO.Convert(c, gender);
        });

        return dto;
    }
}