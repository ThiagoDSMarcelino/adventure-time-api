using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace AdventureTimeApi.Tests;

public class UnitTest1
{
    [Fact]
    public async Task GetCharacters_ReturnsCorrectResponse()
    {
        var client = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>())
            .CreateClient();

        var response = await client.GetAsync("/");

        response.EnsureSuccessStatusCode();
    }
}