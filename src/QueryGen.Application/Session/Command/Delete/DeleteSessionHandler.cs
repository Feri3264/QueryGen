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
        await sessionServices.DeleteAsync(request.Id);

        return Result.Success;
    }
}
