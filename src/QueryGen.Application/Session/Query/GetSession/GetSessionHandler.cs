using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Application.Common.Mappers.Session;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.Session.Query.GetSession;

public class GetSessionHandler
    (ISessionServices sessionServices) : IRequestHandler<GetSessionQuery, ErrorOr<GetSessionResult>>
{
    public async Task<ErrorOr<GetSessionResult>> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.GetById(request.Id);

        if (session.IsError)
            return session.Errors;

        return SessionMapper.ToGetSessionResult(session.Value);
    }
}
