using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AdventureTimeApi.Config;
using AdventureTimeApi.DTOs;
using AdventureTimeApi.Errors;
using AdventureTimeApi.Interfaces;
using AdventureTimeApi.Models;
using AdventureTimeApi.Shared;

namespace AdventureTimeApi.Services;

public class CharactersService : ICharactersRepository
{
    public async Task<IEnumerable<CharacterDTO>> GetCharactersAsync(string? gender, string? specie)
    {
        var charactersSpeciesData = await Util.GetFilePathAsync<CharacterSpecie>(Constants.CHARACTERS_SPECIES_FILE_PATH);
        var charactersData = await Util.GetFilePathAsync<Character>(Constants.CHARACTERS_FILE_PATH);
        var gendersData = await Util.GetFilePathAsync<Gender>(Constants.GENDERS_FILE_PATH);
        var speciesData = await Util.GetFilePathAsync<Specie>(Constants.SPECIES_FILE_PATH);

        if (gender is not null && !gendersData.Any(g => Util.CompareIgnoreCase(g.Name, gender)))
            throw new InvalidGenderException(gender);

        if (specie is not null && !speciesData.Any(s => Util.CompareIgnoreCase(s.Name, specie)))
            throw new InvalidGenderException(specie);

        if (gender is not null)
        {
            var genderId = gendersData.First(g => Util.CompareIgnoreCase(g.Name, gender)).Id;
            charactersData.RemoveAll(c => c.GenderFK != genderId);
        }

        var data = charactersData
            .Select(c =>
            {
                var characterGender = gendersData.First(g => g.Id == c.GenderFK).Name;

                var characterSpecies = charactersSpeciesData
                    .Where(cs => cs.CharacterFK == c.Id)
                    .Select(cs => speciesData.First(s => s.Id == cs.SpecieFK).Name)
                    .ToList();

                return new CharacterDTO(c.Name, characterGender, characterSpecies);
            });

        data = data
            .Where(c => specie is null || c.Species.Any(s => Util.CompareIgnoreCase(s, specie)))
            .OrderBy(c => c.Name);

        return data;
    }
}