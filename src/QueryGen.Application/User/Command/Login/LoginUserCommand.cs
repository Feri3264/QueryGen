using MediatR;
using ErrorOr;
using QueryGen.Application.Common.DTOs.User;

namespace QueryGen.Application.User.Command.Login;

public record LoginUserCommand
    (string Username , string Password) : IRequest<ErrorOr<LoginResult>>;