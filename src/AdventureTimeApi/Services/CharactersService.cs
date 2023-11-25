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
        var characters = await Util.GetFilePathAsync<Character>(Constants.CHARACTERS_FILE_PATH);
        var genders = await Util.GetFilePathAsync<Gender>(Constants.GENDERS_FILE_PATH);
        var charactersSpecies = await Util.GetFilePathAsync<CharacterSpecie>(Constants.CHARACTERS_SPECIES_FILE_PATH);
        var species = await Util.GetFilePathAsync<Specie>(Constants.SPECIES_FILE_PATH);

        var data = characters.Select(c =>
        {
            var characterGender = genders.First(g => g.Id == c.GenderFK).Name;

            var characterSpecies = charactersSpecies
                .Where(cs => cs.CharacterFK == c.Id)
                .Select(cs => species.First(s => s.Id == cs.SpecieFK).Name)
                .ToList();

            return new CharacterDTO(c.Name, characterGender, characterSpecies);
        });

        if (gender is not null)
        {
            // Checks if the gender exists
            if (!genders.Any(g => Util.CompareIgnoreCase(g.Name, gender)))
                throw new InvalidGenderException(gender);

            data = data.Where(c => Util.CompareIgnoreCase(c.Gender, gender));
        }

        if (specie is not null)
        {
            // Checks if the specie exists
            if (!species.Any(s => Util.CompareIgnoreCase(s.Name, specie)))
                throw new InvalidSpecieException(specie);

            data = data.Where(c => c.Species.Any(s => Util.CompareIgnoreCase(s, specie)));
        }

        return data;
    }
}