using AdventureTimeApi.Interfaces;
using AdventureTimeApi.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdventureTimeApi;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
        });

        services.AddTransient<ICharactersRepository, CharactersService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/api/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}