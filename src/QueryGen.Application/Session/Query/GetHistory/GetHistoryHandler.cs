using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Application.Common.Mappers.Session;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.Session.Query.GetHistory;

public class GetHistoryHandler
    (ISessionServices sessionServices) : IRequestHandler<GetHistoryQuery, ErrorOr<GetHistoryResult>>
{
    public async Task<ErrorOr<GetHistoryResult>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
    {
        var history = await sessionServices.GetHistory(request.SessionId, request.UserId, request.HistoryId);

        if (history.IsError)
            return history.Errors;

        return SessionMapper.ToGetHistoryResult(history.Value);
    }
}
