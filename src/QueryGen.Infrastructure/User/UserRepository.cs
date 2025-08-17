using System;
using Microsoft.EntityFrameworkCore;
using QueryGen.Application.Common.Repository;
using QueryGen.Domain.User;
using QueryGen.Infrastructure.Common.Context;

namespace QueryGen.Infrastructure.User;

public class UserRepository
    (QueryGenDbContext db) : IUserRepository
{
    public async Task AddAsync(UserModel model)
    {
        await db.AddAsync(model);
    }

    public async Task<UserModel?> FindByUsername(string Username)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Username == Username);
    }

    public async Task<UserModel?> GetById(Guid Id)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Id == Id);
    }

    public async Task<UserModel?> GetByRefreshToken(string token)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.RefreshToken == token);
    }

    public async Task<bool> IsUserExists(Guid Id)
    {
        return await db.Users.AnyAsync(u => u.Id == Id);
    }

    public async Task<bool> IsUsernameExists(string Username)
    {
        return await db.Users.AnyAsync(u => u.Username == Username);
    }

    public async Task SaveAsync()
    {
        await db.SaveChangesAsync();
    }

    public void Update(UserModel model)
    {
        db.Update(model);
    }
}
