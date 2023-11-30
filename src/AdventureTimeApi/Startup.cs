using AdventureTimeApi.Repositories;
using AdventureTimeApi.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
        
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Adventure Time API", Version = "v1" });
        });

        services.AddLogging(logger =>
        {
            logger.AddConsole();
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