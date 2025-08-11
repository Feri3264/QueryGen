using System;
using Microsoft.EntityFrameworkCore;
using QueryGen.Application.Common.Repository;
using QueryGen.Domain.Session;
using QueryGen.Infrastructure.Common.Context;

namespace QueryGen.Infrastructure.Session;

public class SessionRepository
    (QueryGenDbContext db) : ISessionRepository
{
    public async Task AddAsync(SessionModel model)
    {
        await db.AddAsync(model);
    }

    public void Delete(SessionModel model)
    {
        db.Remove(model);
    }

    public async Task<SessionModel?> GetById(Guid Id)
    {
        return await db.Sessions.FirstOrDefaultAsync(s => s.Id == Id);
    }

    public async Task<List<SessionModel>> GetUserSessions(Guid UserId)
    {
        return await db.Sessions.Where(s => s.UserId == UserId).ToListAsync();
    }

    public async Task<bool> IsSessionExists(Guid Id)
    {
        return await db.Sessions.AnyAsync(s => s.Id == Id);
    }

    public async Task SaveAsync()
    {
        await db.SaveChangesAsync();
    }
}
