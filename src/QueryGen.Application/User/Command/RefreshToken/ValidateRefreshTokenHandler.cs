using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Auth;
using QueryGen.Application.Common.DTOs.User;
using QueryGen.Application.Common.Mappers.User;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.User.Command.RefreshToken;

public class ValidateRefreshTokenHandler
    (IUserServices userServices , IAuthServices authServices) : IRequestHandler<ValidateRefreshTokenCommand, ErrorOr<RefreshTokenResult>>
{
    public async Task<ErrorOr<RefreshTokenResult>> Handle(ValidateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userServices.GetByRefreshToken(request.RefreshToken);

        if (user.IsError)
            return user.Errors;

        var validateRefreshToken = authServices.ValidateRefreshToken(user.Value);

        if (validateRefreshToken.IsError)
            return validateRefreshToken.Errors;


        var jwtToken = authServices.GenerateAccessTokenAsync(user.Value);
        var refreshToken = authServices.GenerateRefreshTokenAsync();

        return UserMapper.ToRefreshTokenResult(jwtToken , refreshToken);
    }
}
