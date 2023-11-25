using System.Collections.Generic;

namespace AdventureTimeApi.DTOs;

public record CharacterDTO(string Name, string Gender, List<string> Species)
{
}