using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Net;
using AdventureTimeApi.DTOs;

namespace AdventureTimeApi.Tests;

public class TestCharactersRoutes
{
    [Fact]
    public async Task GetCharacters_ReturnsCorrectResponse()
    {
        var client = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>())
            .CreateClient();

        var response = await client.GetAsync("api/characters");

        response.EnsureSuccessStatusCode();

        var characters = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

        Assert.NotNull(characters);
    }

    [Fact]
    public async Task GetCharactersByGender_ReturnsCorrectResponse()
    {
        var client = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>())
            .CreateClient();

        var response = await client.GetAsync("api/characters?gender=male");

        response.EnsureSuccessStatusCode();

        var characters = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

        Assert.NotNull(characters);
        Assert.All(characters, c => Assert.Equal("Male", c.Gender));
    }

    [Fact]
    public async Task GetCharactersByGender_ReturnsErrorMessage()
    {
        var client = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>())
            .CreateClient();

        var response = await client.GetAsync("api/characters?gender=invalidGender");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetCharactersBySpecie_ReturnsCorrectResponse()
    {
        var client = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>())
            .CreateClient();

        var response = await client.GetAsync("api/characters?specie=demons");

        response.EnsureSuccessStatusCode();

        var characters = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

        Assert.NotNull(characters);
        Assert.All(characters, c => c.Species.Contains("Demons"));
    }

    [Fact]
    public async Task GetCharactersBySpecie_ReturnsErrorMessage()
    {
        var client = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>())
            .CreateClient();

        var response = await client.GetAsync("api/characters?specie=invalidSpecie");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}