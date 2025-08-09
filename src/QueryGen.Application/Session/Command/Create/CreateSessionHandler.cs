using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Application.Common.Mappers.Session;
using QueryGen.Application.Common.Services;
using QueryGen.Application.Common.Utilities;

namespace QueryGen.Application.Session.Command.Create;

public class CreateSessionHandler
    (ISessionServices sessionServices, IDbServices dbServices) : IRequestHandler<CreateSessionCommand, ErrorOr<CreateSessionResult>>
{
    public async Task<ErrorOr<CreateSessionResult>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var connectionString = ConnectionStringUtility.Build(
            request.Server,
            request.DbName,
            request.useWinAuth,
            request.username,
            request.password,
            request.port
        );

        var metadata = dbServices.GetMetadata(connectionString);

        var session = await sessionServices.CreateAsync(
            request.SessionName,
            request.UserId,
            connectionString,
            metadata
        );

        return SessionMapper.ToCreateResult(session);
    }
}
