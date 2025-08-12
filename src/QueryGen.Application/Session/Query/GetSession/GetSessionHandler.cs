using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Services;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Session.Query.GetSession;

public class GetSessionHandler
    (ISessionServices sessionServices) : IRequestHandler<GetSessionQuery, ErrorOr<SessionModel>>
{
    public async Task<ErrorOr<SessionModel>> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.GetById(request.Id);

        if (session.IsError)
            return session.Errors;

        return session;
    }
}
