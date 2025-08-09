using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.User;

namespace QueryGen.Application.User.Command.Register;

public record RegisterUserCommand
    (string Username , string Password) : IRequest<ErrorOr<RegisterResult>>;
