using MediatR;
using ErrorOr;
using QueryGen.Application.Common.DTOs.Session;

namespace QueryGen.Application.Session.Query.GetHistory;

public record GetHistoryQuery(
    Guid SessionId,
    Guid UserId,
    Guid HistoryId) : IRequest<ErrorOr<GetHistoryResult>>;