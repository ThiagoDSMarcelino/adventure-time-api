using AdventureTimeApi.Interfaces;
using AdventureTimeApi.Models.Characters;
using AdventureTimeApi.Errors;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace AdventureTimeApi.Controllers;

[ApiController]
[Route("api/characters/")]
public class CharactersController : ControllerBase
{
    private readonly ILogger<CharactersController> logger;
    private readonly ICharactersRepository characterRepository;

    public CharactersController(ILogger<CharactersController> logger, ICharactersRepository characterRepository)
    {
        this.logger = logger;
        this.characterRepository = characterRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterDTO>>> Get([FromQuery] string? gender)
    {
        try
        {
            var characters = await characterRepository.GetCharactersAsync(gender);

            logger.LogInformation("Successful operation");

            return Ok(characters);
        }
        catch (InvalidGenderException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (LoadModelException)
        {
            logger.LogError("Error during model loading");
            return StatusCode(500, "Look like the server is having some problems, try again later");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error during action execution");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
