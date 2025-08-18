using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Application.Common.Mappers.Session;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.Session.Command.ChangeLlmModel;

public class ChangeLlmModelHandler
    (ISessionServices sessionServices) : IRequestHandler<ChangeLlmModelCommand, ErrorOr<ChangeModelResult>>
{
    public async Task<ErrorOr<ChangeModelResult>> Handle(ChangeLlmModelCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.ChangeModel(request.SessionId, request.Model, request.UserId);

        if (session.IsError)
            return session.Errors;

        return SessionMapper.ToChangeModelResult(session.Value);
    }
}
