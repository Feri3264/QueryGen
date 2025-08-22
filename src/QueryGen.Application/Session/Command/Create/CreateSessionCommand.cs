using MediatR;
using ErrorOr;
using QueryGen.Application.Common.DTOs.Session;
using QueryGen.Domain.Common.Enums;

namespace QueryGen.Application.Session.Command.Create;

public record CreateSessionCommand(
    string SessionName,
    Guid UserId,
    string ApiToken,
    string LlmModel,
    DatabaseTypeEnum DbType,
    string Server,
    string DbName,
    string username = null,
    string password = null,
    int? port = null) : IRequest<ErrorOr<CreateSessionResult>>;