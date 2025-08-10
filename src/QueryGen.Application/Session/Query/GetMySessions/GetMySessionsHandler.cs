using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Services;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Session.Query.GetMySessions;

public class GetMySessionsHandler
    (ISessionServices sessionServices) : IRequestHandler<GetMySessionsQuery, ErrorOr<List<SessionModel>>>
{
    public async Task<ErrorOr<List<SessionModel>>> Handle(GetMySessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions = await sessionServices.GetUserSessions(request.UserId);

        return sessions;
    }
}
