using MediatR;
using ErrorOr;
using QueryGen.Application.Common.DTOs.Session;

namespace QueryGen.Application.Session.Command.Create;

public record CreateSessionCommand(
    string SessionName,
    Guid UserId,
    string ApiToken,
    string LlmModel,
    string Server,
    string DbName,
    bool useWinAuth = false,
    string username = null,
    string password = null,
    int? port = null) : IRequest<ErrorOr<CreateSessionResult>>;