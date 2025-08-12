using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;

namespace QueryGen.Application.Session.Query.GetSession;

public record GetSessionQuery(Guid Id) : IRequest<ErrorOr<GetSessionResult>>;
