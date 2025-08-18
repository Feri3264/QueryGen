using MediatR;
using ErrorOr;
using QueryGen.Application.Common.DTOs.Session;

namespace QueryGen.Application.Session.Command.ChangeName;

public record ChangeNameCommand(
    Guid SessionId,
    string Name,
    Guid UserId) : IRequest<ErrorOr<ChangeNameResult>>;
