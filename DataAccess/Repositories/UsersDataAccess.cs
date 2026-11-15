using System;
using MarketApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApp.DataAccess.Repositories;

public class UsersDataAccess
{
    PostgresContext _contexte;

    public UsersDataAccess(PostgresContext contexte)
    {
        _contexte = contexte;
    }

    /// <summary>
    /// Fetches all users from the database.
    /// </summary>
    /// <returns>A list of all users.</returns>
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _contexte.Users.ToListAsync();
    }

    /// <summary>
    /// Fetches a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to fetch.</param>
    /// <returns>The user with the specified ID.</returns>
    public async Task<User> GetUserByIdAsync(int id)
    {
        User response = await _contexte.Users.FindAsync(id);

        if (response == null)
        {
            throw new Exception($"User with ID {id} not found.");
        }

        return response;
    }
}
