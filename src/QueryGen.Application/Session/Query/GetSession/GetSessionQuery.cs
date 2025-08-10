using ErrorOr;
using MediatR;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Session.Query.GetSession;

public record GetSessionQuery(Guid Id) : IRequest<ErrorOr<SessionModel>>;
