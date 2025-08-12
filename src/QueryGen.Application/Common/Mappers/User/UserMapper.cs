using System;
using ErrorOr;
using QueryGen.Application.Common.DTOs.User;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Mappers.User;

public static class UserMapper
{
    public static RegisterResult ToRegisterResult(UserModel model, string token)
    {
        return new RegisterResult
        {
            Id = model.Id,
            Username = model.Username,
            Password = model.Password,
            RefreshToken = model.RefreshToken,
            TokenExpire = model.TokenExpire,
            JwtToken = token
        };
    }

    public static LoginResult ToLoginResult(UserModel model, string token)
    {
        return new LoginResult
        {
            Id = model.Id,
            Username = model.Username,
            Password = model.Password,
            RefreshToken = model.RefreshToken,
            TokenExpire = model.TokenExpire,
            JwtToken = token
        };
    }

    public static RefreshTokenResult ToRefreshTokenResult(string JwtToken , string RefreshToken)
    {
        return new RefreshTokenResult
        {
            JwtToken = JwtToken,
            RefreshToken = RefreshToken
        };
    }
}
