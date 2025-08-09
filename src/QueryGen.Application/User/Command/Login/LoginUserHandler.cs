using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.User;

namespace QueryGen.Application.User.Command.Login;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, ErrorOr<LoginResult>>
{
    public Task<ErrorOr<LoginResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
