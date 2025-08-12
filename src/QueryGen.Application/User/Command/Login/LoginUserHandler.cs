using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Auth;
using QueryGen.Application.Common.DTOs.User;
using QueryGen.Application.Common.Mappers.User;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.User.Command.Login;

public class LoginUserHandler
    (IUserServices userServices, IAuthServices authServices) : IRequestHandler<LoginUserCommand, ErrorOr<LoginResult>>
{
    public async Task<ErrorOr<LoginResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userServices.LoginAsync(request.Username, request.Password);

        if (user.IsError)
            return user.Errors;

        var jwtToken = authServices.GenerateAccessTokenAsync(user.Value);

        return UserMapper.ToLoginResult(user.Value , jwtToken);
    }
}
