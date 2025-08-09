using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;

namespace QueryGen.Application.Session.Command.Create;

public class CreateSessionHandler
    () : IRequestHandler<CreateSessionCommand, ErrorOr<CreateSessionResult>>
{
    public Task<ErrorOr<CreateSessionResult>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
