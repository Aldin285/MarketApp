using System;
using Npgsql;

namespace WebApplication1.DataAccess.Configurations;

public class SQLDatabaseConfiguration
{
    private string dataSource = Environment.GetEnvironmentVariable("DATABASELINK")!;
    private string username = Environment.GetEnvironmentVariable("DATABASEUSER")!;
    private string password = Environment.GetEnvironmentVariable("DATABASEPASSWORD")!;
    private string databaseName = Environment.GetEnvironmentVariable("DATABASENAME")!;

    public async Task DataBaseConnection()
    {
        if (string.IsNullOrEmpty(dataSource) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new InvalidOperationException("One or more database environment variables are not set.");
        }

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = dataSource,
            Username = username,
            Password = password,
            Database =  databaseName,
        };

        var connectionString = builder.ConnectionString;

         try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");

                await connection.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nDone.");
    }

}
