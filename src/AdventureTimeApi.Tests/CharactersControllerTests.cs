using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Net;
using AdventureTimeApi.DTOs;
using System;
using System.Net.Http;

namespace AdventureTimeApi.Tests;

public class TestCharactersController
{
    private readonly HttpClient client;
    public TestCharactersController()
    {
        client = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>())
            .CreateClient();
    }

    [Fact]
    public async Task GetAllCharacters_ReturnsCorrectResponse()
    {
        var response = await client.GetAsync("api/characters");

        response.EnsureSuccessStatusCode();

        var characters = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

        Assert.NotNull(characters);
        Assert.NotEmpty(characters);
    }

    [Fact]
    public async Task GetCharacterById_ReturnsCorrectResponse()
    {
        var response = await client.GetAsync("api/characters/1");

        response.EnsureSuccessStatusCode();

        var character = await response.Content.ReadFromJsonAsync<CharacterDTO>();

        Assert.NotNull(character);
        Assert.Equal<uint>(1, character.Id);
    }

    [Fact]
    public async Task GetCharacterById_ReturnsNotFoundForInvalidId()
    {
        var response = await client.GetAsync("api/characters/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SearchCharacters_ReturnsCorrectResponse()
    {
        var response = await client.GetAsync("api/characters/search/Finn");

        response.EnsureSuccessStatusCode();

        var characters = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

        Assert.NotNull(characters);
        Assert.All(characters, c => Assert.Contains("Finn", c.Name, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task SearchCharacters_ReturnsNotFoundForInvalidName()
    {
        var response = await client.GetAsync("api/characters/search/InvalidName");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetCharactersByGender_ReturnsOnlyMaleCharacters()
    {
        var response = await client.GetAsync("api/characters?gender=male");

        response.EnsureSuccessStatusCode();

        var characters = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

        Assert.NotNull(characters);
        Assert.All(characters, c => Assert.Equal("Male", c.Gender));
    }

    [Fact]
    public async Task GetCharactersByInvalidGender_ReturnsNotFound()
    {
        var response = await client.GetAsync("api/characters?gender=invalidGender");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetCharactersBySpecie_ReturnsOnlyDemonsCharacters()
    {
        var response = await client.GetAsync("api/characters?specie=demons");

        response.EnsureSuccessStatusCode();

        var characters = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

        Assert.NotNull(characters);
        Assert.All(characters, c => Assert.Contains("demons", c.Species, StringComparer.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task GetCharactersByInvalidSpecie_ReturnsNotFound()
    {
        var response = await client.GetAsync("api/characters?specie=invalidSpecie");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
