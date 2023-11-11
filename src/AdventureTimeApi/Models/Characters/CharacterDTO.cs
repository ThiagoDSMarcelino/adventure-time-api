namespace AdventureTimeApi.Models.Characters;

public record CharacterDTO(string Name, string Gender)
{
    public static CharacterDTO Convert(Character character, string gender)
        => new(character.Name, gender);
}