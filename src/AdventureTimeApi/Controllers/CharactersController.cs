using System.Collections.Generic;
using System.Linq;
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
    public async Task<ActionResult<IEnumerable<CharacterDTO>>> Get([FromQuery] string? gender, [FromServices] ICharactersRepository characterRepository)
    {
        try
        {
            var characters = await characterRepository.GetCharactersAsync();

            if (gender is not null)
                characters = characters.Where(c => c.Gender == gender);

            return Ok(characters);
        }
        catch
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}
