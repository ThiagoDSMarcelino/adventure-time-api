using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureTimeApi.DTOs;

/// <summary>
/// JSON converter for serializing and deserializing <see cref="CharacterDTO"/>.
/// </summary>
public class CharacterDTOConverter : JsonConverter<CharacterDTO>
{
    /// <inheritdoc/>
    public override CharacterDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        uint id = root.GetProperty("id").GetUInt32();
        string name = root.GetProperty("name").GetString() ?? throw new JsonException("Name is required.");
        string gender = root.GetProperty("gender").GetString() ?? throw new JsonException("Gender is required.");

        var species = root.GetProperty("species").EnumerateArray()
            .Select(specie => specie.GetString())
            .ToList();

        if (species.Count == 0)
            throw new JsonException("At least one specie is required.");

        if (species.Any(s => s is null))
            throw new JsonException("Specie cannot be null.");

        return new CharacterDTO(id, name, gender, species!);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, CharacterDTO value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("gender", value.Gender);

        writer.WriteStartArray("species");
        foreach (string specie in value.Species)
            writer.WriteStringValue(specie);
        writer.WriteEndArray();

        writer.WriteEndObject();
    }
}
