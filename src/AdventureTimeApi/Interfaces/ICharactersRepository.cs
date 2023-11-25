using AdventureTimeApi.Models.Characters;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureTimeApi.Interfaces;

public interface ICharactersRepository
{
    Task<IEnumerable<CharacterDTO>> GetCharactersAsync(string? gender);
}