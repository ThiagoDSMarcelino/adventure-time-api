using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureTimeApi.Models.Characters;

namespace AdventureTimeApi.Interfaces;

public interface ICharactersRepository
{
    Task<IEnumerable<CharacterDTO>> GetCharactersAsync();
}