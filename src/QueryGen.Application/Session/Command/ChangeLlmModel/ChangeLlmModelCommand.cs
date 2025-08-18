using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.Session;

namespace QueryGen.Application.Session.Command.ChangeLlmModel;

public record ChangeLlmModelCommand(
    Guid SessionId,
    string Model,
    Guid UserId) : IRequest<ErrorOr<ChangeModelResult>>;