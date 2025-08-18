using System;
using System.Text.Json.Nodes;
using ErrorOr;
using QueryGen.Application.Common.Repository;
using QueryGen.Application.Common.Services;
using QueryGen.Domain.Session;
using QueryGen.Domain.User;

namespace QueryGen.Infrastructure.Common.Services;

public class SessionServices(
    ISessionRepository sessionRepository,
    IUserServices userServices) : ISessionServices
{
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

    public async Task<ErrorOr<List<SessionModel>?>> GetUserSessions(Guid UserId)
    {
        var sessions = await sessionRepository.GetUserSessions(UserId);

        return sessions;
    }
}
