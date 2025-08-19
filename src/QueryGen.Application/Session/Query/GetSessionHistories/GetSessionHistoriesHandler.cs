using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Application.Common.Mappers.Session;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.Session.Query.GetSessionHistories;

public class GetSessionHistoriesHandler
    (ISessionServices sessionServices) : IRequestHandler<GetSessionHistoriesQuery, ErrorOr<List<GetSessionHistoriesResult>?>>
{
    public async Task<ErrorOr<List<GetSessionHistoriesResult>?>> Handle(GetSessionHistoriesQuery request, CancellationToken cancellationToken)
    {
        var histories = await sessionServices.GetSessionHistories(request.SessionId, request.UserId);

        if (histories.IsError)
            return histories.Errors;

        return SessionMapper.ToGetHistories(histories.Value);
    }
}
