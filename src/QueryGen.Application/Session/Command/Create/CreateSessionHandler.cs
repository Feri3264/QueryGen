using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Application.Common.Mappers.Session;
using QueryGen.Application.Common.Services;
using QueryGen.Application.Common.Utilities;
using QueryGen.Application.Common.Utilities.ConnectionString;

namespace QueryGen.Application.Session.Command.Create;

public class CreateSessionHandler
    (ISessionServices sessionServices, IDbServices dbServices) : IRequestHandler<CreateSessionCommand, ErrorOr<CreateSessionResult>>
{
    public async Task<ErrorOr<CreateSessionResult>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var builder = ConnectionStringBuilderFactory.Create(request.DbType);

        if (builder.IsError)
            return builder.Errors;

        var connectionString = builder.Value.Build(
            request.Server,
            request.DbName,
            request.username,
            request.password,
            request.port
        );

        var metadata = await dbServices.GetMetadata(connectionString.Value , request.DbType);

        if (metadata.IsError)
            return metadata.Errors;

        var session = await sessionServices.CreateAsync(
            request.SessionName,
            request.UserId,
            connectionString.Value,
            metadata.Value,
            request.ApiToken,
            request.LlmModel,
            request.DbType,
            request.LlmType
        );

        if (session.IsError)
            return session.Errors;

        return SessionMapper.ToCreateResult(session.Value);
    }
}
