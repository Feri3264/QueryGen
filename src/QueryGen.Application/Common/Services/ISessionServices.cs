using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using ErrorOr;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Common.Services;

public interface ISessionServices
{
    Task<ErrorOr<SessionModel>> GetById(Guid Id);

    Task<ErrorOr<List<SessionModel>?>> GetUserSessions(Guid UserId);
    
    Task<ErrorOr<SessionModel>> CreateAsync(string Name, Guid UserId, string ConnectionString, string Metadata);

    Task<ErrorOr<Success>> DeleteAsync(Guid Id);
}
