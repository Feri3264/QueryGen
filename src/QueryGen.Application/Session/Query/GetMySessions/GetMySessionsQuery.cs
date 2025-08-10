using ErrorOr;
using MediatR;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Session.Query.GetMySessions;

public record GetMySessionsQuery(Guid UserId) : IRequest<ErrorOr<List<SessionModel>?>>;
