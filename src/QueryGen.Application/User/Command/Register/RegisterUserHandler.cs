using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Auth;
using QueryGen.Application.Common.DTOs.User;
using QueryGen.Application.Common.Mappers.User;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.User.Command.Register;

public class RegisterUserHandler(IUserServices userServices, IAuthServices authServices) : IRequestHandler<RegisterUserCommand, ErrorOr<RegisterResult>>
{
    public async Task<ErrorOr<RegisterResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userServices.RegisterAsync(request.Username, request.Password);

        if (user.IsError)
            return user.Errors;

        var jwtToken = authServices.GenerateAccessTokenAsync(user.Value);

        return UserMapper.ToRegisterResult(user.Value , jwtToken);
    }
}
