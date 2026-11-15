using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using MarketApp.DataAccess.Seeds;
using MarketApp.Models;
using MarketApp.DataAccess.Repositories;
using System.Text.Json.Serialization;
using AutoMapper;
using WebApplication1.DTO.User;
using MarketApp.Mapping;

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
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Keep property names as defined in the model
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                // For better readability of JSON output 
                options.JsonSerializerOptions.WriteIndented = true; 
                // To display enum names instead of numbers in JSON output
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
        ;

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
        services.AddScoped<ArticleDataAccess>();
        services.AddDbContext<PostgresContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        // services.AddScoped<IMarketService, MarketService>();
        

        // AutoMapper configuration
        services.AddAutoMapper(cfg => cfg.LicenseKey = Environment.GetEnvironmentVariable("AUTOMAPPER_LICENSE_KEY"), typeof(Program));
        services.AddScoped<UserMappingProfile>();


    }
}
