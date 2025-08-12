using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Application.Common.Mappers.Session;
using QueryGen.Application.Common.Services;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Session.Query.GetMySessions;

public class GetMySessionsHandler
    (ISessionServices sessionServices) : IRequestHandler<GetMySessionsQuery, ErrorOr<List<GetMySessionsResult>?>>
{
    public async Task<ErrorOr<List<GetMySessionsResult>?>> Handle(GetMySessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions = await sessionServices.GetUserSessions(request.UserId);

        if (sessions.IsError)
            return sessions.Errors;

        return SessionMapper.ToGetMySessionsResult(sessions.Value);
    }
}
