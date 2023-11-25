using System.Text.Json.Serialization;

namespace AdventureTimeApi.Models;

public record CharacterSpecie
{
    [JsonPropertyName("id")]
    public uint Id { get; set; }

    [JsonPropertyName("character_fk")]
    public uint CharacterFK { get; set; }

    [JsonPropertyName("specie_fk")]
    public uint SpecieFK { get; set; }
}