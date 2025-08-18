using System;
using ErrorOr;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Domain.Session;

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
            LlmModel = model.LlmModel
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
            LlmModel = model.LlmModel
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
            UserId = model.UserId
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
            UserId = model.UserId
        };
    }
}
