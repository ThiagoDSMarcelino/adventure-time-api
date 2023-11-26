using System;

namespace AdventureTimeApi.Errors;

public class InvalidCharacterNameException : Exception
{
    public InvalidCharacterNameException(string name)
        : base($"Has no character with the name \"{name}\" does not exist castrated. If this character is a new character, please add it to the database by making a pull request in the GitHub repository https://github.com/ThiagoDSMarcelino/adventure-time-api/pulls.")
    {
    }
}
