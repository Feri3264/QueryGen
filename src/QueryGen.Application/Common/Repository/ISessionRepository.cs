using System;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Common.Repository;

public interface ISessionRepository
{
    Task<SessionModel> GetById(Guid Id);

    Task<List<SessionModel>> GetUserSessions(Guid UserId);

    Task<bool> IsSessionExists(Guid Id);

    Task AddAsync(SessionModel model);

    void Delete(SessionModel model);

    Task SaveAsync();
}
