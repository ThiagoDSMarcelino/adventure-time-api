using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureTimeApi.DTOs;

namespace AdventureTimeApi.Interfaces;

public interface ICharactersRepository
{
    Task<IEnumerable<CharacterDTO>> GetCharactersAsync(string? gender, string? specie);
}