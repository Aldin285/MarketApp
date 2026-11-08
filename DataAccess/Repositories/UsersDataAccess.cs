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

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _contexte.Users.ToListAsync();
    }

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
