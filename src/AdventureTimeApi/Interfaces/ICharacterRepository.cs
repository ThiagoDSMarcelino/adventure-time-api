using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureTimeApi.Models;

namespace AdventureTimeApi.Interfaces;

public interface ICharacterRepository
{
    Task<List<Character>> GetCharactersAsync();
}