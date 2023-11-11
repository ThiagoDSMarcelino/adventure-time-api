using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureTimeApi.Interfaces;
using AdventureTimeApi.Models.Characters;
using Microsoft.AspNetCore.Mvc;

namespace AdventureTimeApi.Controllers;

[ApiController]
[Route("api/characters/")]
public class CharactersController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterDTO>>> Get([FromServices] ICharactersRepository characterRepository)
    {
        try
        {
            var characters = await characterRepository.GetCharactersAsync();

            return Ok(characters);
        }
        catch
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}
