using System;
using System.Linq;
using MarketApp.DTO.User;
using MarketApp.Models;

namespace MarketApp.DataAccess.Seeds;

public class MarketDbInitializer
{
    public static void Initialize(PostgresContext context)
    {
        // Ensure the database is created
        context.Database.EnsureCreated();

        // Seed data if necessary
        if (!context.Users.Any())
        {
            // Add initial users
            context.Users.AddRange(
                new Models.User { Name = "Admin", Email = "admin@example.com", Type = UserTypes.admin.ToString(), CreatedAt = DateTime.UtcNow },
                new Models.User { Name = "John Doe", Email = "john.doe@example.com", Type = UserTypes.customer.ToString(), CreatedAt = DateTime.UtcNow },
                new Models.User { Name = "Jane Smith", Email = "jane.smith@example.com", Type = UserTypes.employee.ToString(), CreatedAt = DateTime.UtcNow }
            );

            context.SaveChanges();
        }

        if (!context.Categories.Any())
        {
            // Add initial categories
            context.Categories.AddRange(
                new Models.Category { Name = "Consumables" },
                new Models.Category { Name = "Electronics" },
                new Models.Category { Name = "Clothing" }
            );

            context.SaveChanges();
        }

        if (!context.Articles.Any())
        {
            // Add initial articles
            context.Articles.AddRange(
                new Models.Article { Title = "Apple", Description = "Fresh red apple", Price = 0.5m, CategoryId = context.Categories.First(c => c.Name == "Consumables").Id },
                new Models.Article { Title = "Laptop", Description = "High-performance laptop", Price = 1200m, CategoryId = context.Categories.First(c => c.Name == "Electronics").Id },
                new Models.Article { Title = "T-Shirt", Description = "Comfortable cotton t-shirt", Price = 15m, CategoryId = context.Categories.First(c => c.Name == "Clothing").Id }
            );

            context.SaveChanges();
        }
    }

}
