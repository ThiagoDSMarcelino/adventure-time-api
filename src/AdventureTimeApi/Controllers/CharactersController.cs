using AdventureTimeApi.Repositories;
using AdventureTimeApi.Errors;
using AdventureTimeApi.DTOs;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace AdventureTimeApi.Controllers;

[ApiController]
[Route("api/characters/")]
[Produces("application/json")]
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
    public async Task<ActionResult<IEnumerable<CharacterDTO>>> Get([FromQuery] string? gender, [FromQuery] string? specie)
    {
        try
        {
            var characters = await characterRepository.GetCharactersAsync(gender, specie);

            logger.LogInformation("Successful operation");

            return Ok(characters);
        }
        catch (InvalidGenderException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (InvalidSpecieException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (LoadModelException)
        {
            logger.LogError("Error during model loading for CharactersController.Get");
            return StatusCode(500, "Look like the server is having some problems, try again later");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unhandled exception at CharactersController.Get");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
