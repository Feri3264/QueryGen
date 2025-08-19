using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;

namespace QueryGen.Application.Session.Query.GetSessionHistories;

public record GetSessionHistoriesQuery(
    Guid SessionId,
    Guid UserId) : IRequest<ErrorOr<List<GetSessionHistoriesResult>?>>;