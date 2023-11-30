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
    private readonly ILogger<CharactersController> _logger;
    private readonly ICharactersRepository _characterRepository;

    public CharactersController(ILogger<CharactersController> logger, ICharactersRepository characterRepository)
    {
        _logger = logger;
        _characterRepository = characterRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CharacterDTO>>> Get([FromQuery] string? gender, [FromQuery] string? specie, [FromQuery] string sort = "name")
    {
        try
        {
            var characters = await _characterRepository.ListAsync(gender, specie, sort);

            return Ok(characters);
        }
        catch (InvalidGenderException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidSpecieException e)
        {
            return NotFound(e.Message);
        }
        catch (LoadModelException e)
        {
            _logger.LogError("Error during model loading: {ErrorMessage}. Route: CharactersController.Get", e.Message);

            return StatusCode(500, "Looks like the server is having some problems; try again later.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception at CharactersController.Get");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDTO>> GetById(uint id)
    {
        try
        {
            var character = await _characterRepository.GetAsync(id);

            return Ok(character);
        }
        catch (InvalidCharacterIdException e)
        {
            return NotFound(e.Message);
        }
        catch (LoadModelException e)
        {
            _logger.LogError("Error during model loading: {ErrorMessage}. Route: CharactersController.GetById", e.Message);
            return StatusCode(500, "Looks like the server is having some problems; try again later.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception at CharactersController.Get");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("search/{name}")]
    public async Task<ActionResult<List<CharacterDTO>>> Search(string name)
    {
        try
        {
            var characters = await _characterRepository.SearchAsync(name);

            return Ok(characters);
        }
        catch (InvalidCharacterNameException e)
        {
            return NotFound(e.Message);
        }
        catch (LoadModelException e)
        {
            _logger.LogError("Error during model loading: {ErrorMessage}. Route: CharactersController.Search", e.Message);
            return StatusCode(500, "Looks like the server is having some problems; try again later.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception at CharactersController.Search");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
