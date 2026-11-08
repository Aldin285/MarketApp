using System;
using Microsoft.EntityFrameworkCore;
using MarketApp.DataAccess.Seeds;
using MarketApp.Models;
using MarketApp.DataAccess.Repositories;

namespace MarketApp;

public class Startup
{

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllersWithViews();
        services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        // Swagger/OpenAPI configuration
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // To include XML comments in Swagger, ensure that the XML documentation file is generated in the project settings.
            var xmlFileName = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });

        // Database configuration
        services.AddScoped<MarketDbInitializer>();
        services.AddScoped<UsersDataAccess>();
        services.AddDbContext<PostgresContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        // services.AddScoped<IMarketService, MarketService>();
    }
}
