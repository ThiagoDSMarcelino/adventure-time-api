using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureTimeApi.DTOs;

namespace AdventureTimeApi.Repositories
{
    /// <summary>
    /// Interface for interacting with characters in the Adventure Time API.
    /// </summary>
    public interface ICharactersRepository
    {
        /// <summary>
        /// Retrieves a list of characters based on optional gender and species filters.
        /// </summary>
        /// <param name="gender">Optional filter by gender.</param>
        /// <param name="specie">Optional filter by species.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of CharacterDTO.</returns>
        Task<List<CharacterDTO>> ListAsync(string? gender, string? specie, string sort, int page, int pageSize);

        /// <summary>
        /// Retrieves a character by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the character.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CharacterDTO with the specified ID.</returns>
        Task<CharacterDTO> GetAsync(uint id);

        /// <summary>
        /// Searches for a character by its name.
        /// </summary>
        /// <param name="name">The name of the character to search for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CharacterDTO with the specified name.</returns>
        Task<List<CharacterDTO>> SearchAsync(string name);
    }
}
