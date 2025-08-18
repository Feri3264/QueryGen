using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Application.Common.Mappers.Session;
using QueryGen.Application.Common.Services;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Session.Command.ChangeName;

public class ChangeNameHandler
    (ISessionServices sessionServices) : IRequestHandler<ChangeNameCommand, ErrorOr<ChangeNameResult>>
{
    public async Task<ErrorOr<ChangeNameResult>> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.ChangeName(request.SessionId, request.Name, request.UserId);

        if (session.IsError)
            return session.Errors;

        return SessionMapper.ToChangeNameResult(session.Value);
    }
}
