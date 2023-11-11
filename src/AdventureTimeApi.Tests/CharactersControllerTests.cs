using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Collections.Generic;
using AdventureTimeApi.Models.Characters;

namespace AdventureTimeApi.Tests;

public class TestCharactersRoutes
{
    [Fact]
    public async Task GetCharacters_ReturnsCorrectResponse()
    {
        var client = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>())
            .CreateClient();

        var response = await client.GetAsync("api/characters/");

        response.EnsureSuccessStatusCode();

        var characters = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

        Assert.NotNull(characters);
    }
}