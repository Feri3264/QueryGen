using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.Session.Command.Delete;

public class DeleteSessionHandler
    (ISessionServices sessionServices) : IRequestHandler<DeleteSessionCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.DeleteAsync(request.Id);

        if (session.IsError)
            return session.Errors;

        return Result.Success;
    }
}
