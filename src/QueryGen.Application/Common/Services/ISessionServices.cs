using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Common.Services;

public interface ISessionServices
{
    Task<SessionModel> GetById(Guid Id);
    
    Task<SessionModel> CreateAsync
        (string Name, Guid UserId, string ConnectionString, JsonArray Metadata);

    Task DeleteAsync(Guid Id);
}
