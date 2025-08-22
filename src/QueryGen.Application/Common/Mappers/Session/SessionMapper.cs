using System;
using System.Runtime.CompilerServices;
using ErrorOr;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Domain.Common.Enums;
using QueryGen.Domain.Session;
using QueryGen.Domain.SessionHistory;

namespace QueryGen.Application.Common.Mappers.Session;

public static class SessionMapper
{
    public static CreateSessionResult ToCreateResult(SessionModel model)
    {
        return new CreateSessionResult
        {
            Id = model.Id,
            Name = model.Name,
            ConnectionString = model.ConnectionString,
            Metadata = model.Metadata,
            UserId = model.UserId,
            ApiToken = model.ApiToken,
            LlmModel = model.LlmModel,
            DbType = model.DbType
        };
    }

    public static GetSessionResult ToGetSessionResult(SessionModel model)
    {
        return new GetSessionResult
        {
            Name = model.Name,
            ConnectionString = model.ConnectionString,
            Metadata = model.Metadata,
            UserId = model.UserId,
            ApiToken = model.ApiToken,
            LlmModel = model.LlmModel,
            DbType = model.DbType
        };
    }

    public static List<GetMySessionsResult>? ToGetMySessionsResult(List<SessionModel>? model)
    {
        var sessions = new List<GetMySessionsResult>();

        if (model is not null)
        {
            sessions = model.Select(s => new GetMySessionsResult
            {
                Name = s.Name,
                ConnectionString = s.ConnectionString,
                Metadata = s.Metadata,
                ApiToken = s.ApiToken,
                LlmModel = s.LlmModel,
                DbType = s.DbType,
                UserId = s.UserId
            }).ToList();
        }

        return sessions;
    }

    public static ChangeNameResult ToChangeNameResult(SessionModel model)
    {
        return new ChangeNameResult
        {
            Name = model.Name,
            ConnectionString = model.ConnectionString,
            Metadata = model.Metadata,
            ApiToken = model.ApiToken,
            LlmModel = model.LlmModel,
            UserId = model.UserId,
            DbType = model.DbType
        };
    }

    public static ChangeModelResult ToChangeModelResult(SessionModel model)
    {
        return new ChangeModelResult
        {
            Name = model.Name,
            ConnectionString = model.ConnectionString,
            Metadata = model.Metadata,
            ApiToken = model.ApiToken,
            LlmModel = model.LlmModel,
            UserId = model.UserId,
            DbType = model.DbType
        };
    }

    public static GetHistoryResult ToGetHistoryResult(SessionHistoryModel model)
    {
        return new GetHistoryResult
        {
            Id = model.Id,
            Prompt = model.Prompt,
            Query = model.Query,
            Result = model.Result,
            CreatedAt = model.CreatedAt,
            SessionId = model.SessionId
        };
    }

    public static List<GetSessionHistoriesResult>? ToGetHistories(List<SessionHistoryModel>? model)
    {
        var histories = new List<GetSessionHistoriesResult>();

        if (model is not null)
        {
            histories = model.Select(h => new GetSessionHistoriesResult
            {
                Id = h.Id,
                Prompt = h.Prompt,
                Query = h.Query,
                Result = h.Result,
                CreatedAt = h.CreatedAt,
                SessionId = h.SessionId
            }).ToList();
        }

        return histories;
    }
}
