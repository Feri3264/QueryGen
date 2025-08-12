using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Session.Query.GetMySessions;

public record GetMySessionsQuery(Guid UserId) : IRequest<ErrorOr<List<GetMySessionsResult>?>>;
