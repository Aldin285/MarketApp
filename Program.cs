using MarketApp.DataAccess.Seeds;
using MarketApp;
using Microsoft.EntityFrameworkCore;
using MarketApp.Models;

// Load .env before ASP.NET Core creates its configuration and host.
var envDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
string? envFile = null;

while (envDirectory is not null)
{
    var candidate = Path.Combine(envDirectory.FullName, ".env");
    if (File.Exists(candidate))
    {
        envFile = candidate;
        break;
    }

    envDirectory = envDirectory.Parent;
}

if (envFile is null)
{
    throw new InvalidOperationException("The required .env configuration file was not found.");
}

DotNetEnv.Env.Load(envFile);

var builder = WebApplication.CreateBuilder(args);

// Do not use appsettings.json or launchSettings.json for application configuration.
builder.Configuration.Sources.Clear();
builder.Configuration.AddEnvironmentVariables();

var databaseHost = Environment.GetEnvironmentVariable("DATABASELINK");
var databasePort = Environment.GetEnvironmentVariable("DATABASEPORT") ?? "5432";
var databaseUser = Environment.GetEnvironmentVariable("DATABASEUSER");
var databasePassword = Environment.GetEnvironmentVariable("DATABASEPASSWORD");
var databaseName = Environment.GetEnvironmentVariable("DATABASENAME");

if (string.IsNullOrWhiteSpace(databaseHost)
    || string.IsNullOrWhiteSpace(databaseUser)
    || string.IsNullOrWhiteSpace(databasePassword)
    || string.IsNullOrWhiteSpace(databaseName))
{
    throw new InvalidOperationException(
        "Missing database configuration. Set DATABASELINK, DATABASEUSER, DATABASEPASSWORD, and DATABASENAME in .env.");
}

// Set the connection string for the database in the configuration
builder.Configuration["ConnectionStrings:DefaultConnection"] =
    $"Host={databaseHost};Port={databasePort};Database={databaseName};Username={databaseUser};Password={databasePassword}";

Startup startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

CreateDbIfNotExists(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


// Setting up Swagger for API documentation
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    // Configure the Swagger endpoint and UI settings
    // Makes the Swagger UI available at the root URL (e.g., https://localhost:5001/index.html)
    // Will keep it at the default path for now (https://localhost:5001/swagger/index.html) 

    // options.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan API v1");

    // options.RoutePrefix = string.Empty;
}

);

app.UseAuthorization();

app.MapControllers();

app.Run();


// ---------------------------------------------

static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<PostgresContext>();
                    MarketDbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }