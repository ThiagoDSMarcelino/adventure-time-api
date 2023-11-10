using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureTimeApi.Interfaces;
using AdventureTimeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdventureTimeApi.Controllers;

[ApiController]
[Route("api/characters/")]
public class CharactersController : ControllerBase
{
    [HttpGet("/")]
    public async Task<ActionResult<List<Character>>> Get([FromServices] ICharacterRepository characterRepository)
    {
        try
        {
            List<Character> characters = await characterRepository.GetCharactersAsync();

            return Ok(characters);
        }
        catch
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}
