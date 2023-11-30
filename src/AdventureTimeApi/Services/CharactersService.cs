using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AdventureTimeApi.Config;
using AdventureTimeApi.DTOs;
using AdventureTimeApi.Errors;
using AdventureTimeApi.Repositories;
using AdventureTimeApi.Models;
using AdventureTimeApi.Shared;

namespace AdventureTimeApi.Services;

public class CharactersService : ICharactersRepository
{
    private static async Task<(List<Character> charactersData, List<Gender> gendersData, List<CharacterSpecie> charactersSpeciesData, List<Specie> speciesData)> LoadData()
    {
        var charactersSpeciesData = await Util.GetFilePathAsync<CharacterSpecie>(Constants.CHARACTERS_SPECIES_FILE_PATH);
        var charactersData = await Util.GetFilePathAsync<Character>(Constants.CHARACTERS_FILE_PATH);
        var gendersData = await Util.GetFilePathAsync<Gender>(Constants.GENDERS_FILE_PATH);
        var speciesData = await Util.GetFilePathAsync<Specie>(Constants.SPECIES_FILE_PATH);

        return (charactersData, gendersData, charactersSpeciesData, speciesData);
    }

    public async Task<CharacterDTO> GetAsync(uint id)
    {
        var (charactersData, gendersData, charactersSpeciesData, speciesData) = await LoadData();

        var character = charactersData.FirstOrDefault(c => c.Id == id) ?? throw new InvalidCharacterIdException(id);
        var dto = new CharacterDTO(character, gendersData, charactersSpeciesData, speciesData);

        return dto;
    }

    public async Task<List<CharacterDTO>> ListAsync(string? gender, string? specie, string sort)
    {
        var (charactersData, gendersData, charactersSpeciesData, speciesData) = await LoadData();

        if (specie is not null && !speciesData.Any(s => Util.CompareIgnoreCase(s.Name, specie)))
            throw new InvalidSpecieException(specie);

        if (gender is not null)
        {
            if (!gendersData.Any(g => Util.CompareIgnoreCase(g.Name, gender)))
                throw new InvalidGenderException(gender);

            var genderId = gendersData.First(g => Util.CompareIgnoreCase(g.Name, gender)).Id;
            charactersData.RemoveAll(c => c.GenderFK != genderId);
        }

        var data = charactersData.Select(c => new CharacterDTO(c, gendersData, charactersSpeciesData, speciesData));

        var dto = data.Where(c => specie is null || c.Species.Any(s => Util.CompareIgnoreCase(s, specie)));

        dto = sort switch
        {
            "id" => dto.OrderBy(c => c.Id),
            _ => dto.OrderBy(c => c.Name),
        };

        return dto.ToList();
    }

    public async Task<List<CharacterDTO>> SearchAsync(string name)
    {
        var (charactersData, gendersData, charactersSpeciesData, speciesData) = await LoadData();

        var characters = charactersData
            .Where(c => c.Name.Split(" ").Any(n => Util.CompareIgnoreCase(n, name)));

        if (!characters.Any())
            throw new InvalidCharacterNameException(name);

        var dto = characters
            .Select(c => new CharacterDTO(c, gendersData, charactersSpeciesData, speciesData))
            .ToList();

        return dto;
    }
}