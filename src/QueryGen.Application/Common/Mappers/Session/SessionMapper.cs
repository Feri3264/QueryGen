using System;
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
            UserId = model.UserId
        };
    }
}
