using System;
using System.Runtime.Serialization;
using System.Text.Json.Nodes;
using ErrorOr;
using QueryGen.Application.Common.Repository;
using QueryGen.Application.Common.Services;
using QueryGen.Domain.Session;
using QueryGen.Domain.SessionHistory;
using QueryGen.Domain.User;

namespace QueryGen.Infrastructure.Common.Services;

public class SessionServices(
    ISessionRepository sessionRepository,
    IUserServices userServices,
    ISessionHistoryRepository sessionHistoryRepository) : ISessionServices
{
    public async Task<ErrorOr<SessionModel>> ChangeModel(Guid SessionId, string Model, Guid UserId)
    {
        var session = await sessionRepository.GetById(SessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        if (UserId != session.UserId)
            return SessionError.SessionThiefError;

        session.SetModel(Model);

        sessionRepository.Update(session);
        await sessionRepository.SaveAsync();

        return session;     
    }

    public async Task<ErrorOr<SessionModel>> ChangeName(Guid SessionId, string Name, Guid UserId)
    {
        var session = await sessionRepository.GetById(SessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        if (UserId != session.UserId)
            return SessionError.SessionThiefError;

        session.SetName(Name);

        sessionRepository.Update(session);
        await sessionRepository.SaveAsync();

        return session;
    }

    public async Task<ErrorOr<SessionModel>> CreateAsync(string Name, Guid UserId, string ConnectionString, string Metadata , string ApiToken , string LlmModel)
    {
        if (!await userServices.IsUserExists(UserId))
            return UserError.UserNotFound;

        var session = SessionModel.Create(
            Name,
            ConnectionString,
            Metadata,
            UserId,
            ApiToken,
            LlmModel
        );

        if (session.IsError)
            return session.Errors;

        await sessionRepository.AddAsync(session.Value);
        await sessionRepository.SaveAsync();

        return session;
    }

    public async Task<ErrorOr<SessionHistoryModel>> CreateHistoryAsync(Guid SessionId, string Prompt, string Query, string Result)
    {
        var history = SessionHistoryModel.Create(
            Prompt,
            Query,
            Result,
            DateTime.Now,
            SessionId
        );

        if (history.IsError)
            return history.Errors;

        await sessionHistoryRepository.AddAsync(history.Value);
        await sessionHistoryRepository.SaveAsync();

        return history;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(Guid Id)
    {
        var session = await sessionRepository.GetById(Id);

        if (session is null)
            return SessionError.SessionNotFound;

        sessionRepository.Delete(session);
        await sessionRepository.SaveAsync();

        return Result.Success;
    }

    public async Task<ErrorOr<SessionModel>> GetById(Guid Id , Guid UserId)
    {
        var session = await sessionRepository.GetById(Id);

        if (session is null)
            return SessionError.SessionNotFound;

        if (session.UserId != UserId)
            return SessionError.SessionThiefError;

        return session;
    }

    public async Task<ErrorOr<SessionHistoryModel>> GetHistory(Guid SessionId, Guid UserId, Guid HistoryId)
    {
        var session = await sessionRepository.GetById(SessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        if (UserId != session.UserId)
            return SessionError.SessionThiefError;

        var history = await sessionHistoryRepository.GetById(HistoryId);

        if (history is null)
            return SessionHistoryError.HistoryNotFound;

        return history;
    }

    public async Task<ErrorOr<List<SessionHistoryModel>?>> GetSessionHistories(Guid SessionId, Guid UserId)
    {
        var session = await sessionRepository.GetById(SessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        if (UserId != session.UserId)
            return SessionError.SessionThiefError;

        var histories = await sessionHistoryRepository.GetSessionHistories(SessionId);

        return histories;
    }

    public async Task<ErrorOr<List<SessionModel>?>> GetUserSessions(Guid UserId)
    {
        var sessions = await sessionRepository.GetUserSessions(UserId);

        return sessions;
    }
}
