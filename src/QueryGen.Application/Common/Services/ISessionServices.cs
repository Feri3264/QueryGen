using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using ErrorOr;
using QueryGen.Domain.Session;
using QueryGen.Domain.SessionHistory;

namespace QueryGen.Application.Common.Services;

public interface ISessionServices
{
    Task<ErrorOr<SessionModel>> GetById(Guid Id, Guid UserId);

    Task<ErrorOr<List<SessionModel>?>> GetUserSessions(Guid UserId);

    Task<ErrorOr<SessionModel>> CreateAsync(string Name, Guid UserId, string ConnectionString, string Metadata, string ApiToken, string LlmModel);

    Task<ErrorOr<SessionHistoryModel>> CreateHistoryAsync(Guid SessionId, string Prompt, string Query, string Result);

    Task<ErrorOr<Success>> DeleteAsync(Guid Id);

    Task<ErrorOr<SessionModel>> ChangeName(Guid SessionId, string Name, Guid UserId);

    Task<ErrorOr<SessionModel>> ChangeModel(Guid SessionId, string Model, Guid UserId);

    Task<ErrorOr<SessionHistoryModel>> GetHistory(Guid SessionId, Guid UserId, Guid HistoryId);

    Task<ErrorOr<List<SessionHistoryModel>?>> GetSessionHistories(Guid SessionId , Guid UserId);
}
