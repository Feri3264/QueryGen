using ErrorOr;
using MediatR;

namespace QueryGen.Application.Session.Command.Delete;

public record DeleteSessionCommand(Guid Id) : IRequest<ErrorOr<Success>>;
