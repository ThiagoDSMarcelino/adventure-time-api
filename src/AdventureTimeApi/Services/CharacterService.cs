using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using AdventureTimeApi.Config;
using AdventureTimeApi.Interfaces;
using AdventureTimeApi.Models;

namespace AdventureTimeApi.Services;

public class CharacterService : ICharacterRepository
{
    public async Task<List<Character>> GetCharactersAsync()
    {
        using FileStream openStream = File.OpenRead(Constants.CHARACTER_FILE_PATH);
        List<Character> characters = await JsonSerializer.DeserializeAsync<List<Character>>(openStream) ?? throw new Exception(); // TODO: Handle exception

        return characters;
    }
}