using System;
using ErrorOr;
using QueryGen.Application.Common.DTOs.User;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Mappers.User;

public static class UserMapper
{
    public static RegisterResult ToRegisterResult(UserModel model)
    {
        return new RegisterResult
        {
            Username = model.Username,
            Password = model.Password
        };
    }

    public static LoginResult ToLoginResult(UserModel model)
    {
        return new LoginResult
        {
            Id = model.Id,
            Username = model.Username,
            Password = model.Password,
            RefreshToken = model.RefreshToken,
            TokenExpire = model.TokenExpire
        };
    }
}
