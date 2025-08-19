using System;
using Microsoft.EntityFrameworkCore;
using QueryGen.Application.Common.Repository;
using QueryGen.Domain.SessionHistory;
using QueryGen.Infrastructure.Common.Context;

namespace QueryGen.Infrastructure.SessionHistory;

public class SessionHistoryRepository
    (QueryGenDbContext db) : ISessionHistoryRepository
{
    public async Task AddAsync(SessionHistoryModel model)
    {
        await db.AddAsync(model);
    }

    public async Task<SessionHistoryModel?> GetById(Guid Id)
    {
        return await db.SessionsHistories.FirstOrDefaultAsync(h => h.Id == Id);
    }

    public Task<List<SessionHistoryModel>> GetSessionHistories(Guid SessionId)
    {
        return db.SessionsHistories.Where(h => h.SessionId == SessionId).ToListAsync();
    }

    public async Task SaveAsync()
    {
        await db.SaveChangesAsync();
    }
}
