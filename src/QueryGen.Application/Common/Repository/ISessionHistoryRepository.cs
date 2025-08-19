using System;
using QueryGen.Domain.SessionHistory;

namespace QueryGen.Application.Common.Repository;

public interface ISessionHistoryRepository
{
    Task<SessionHistoryModel?> GetById(Guid Id);

    Task<List<SessionHistoryModel>> GetSessionHistories(Guid SessionId);

    Task AddAsync(SessionHistoryModel model);

    Task SaveAsync();
}
