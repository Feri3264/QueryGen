using System;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Auth;

public interface IAuthServices
{
    Task<string> GenerateAccessTokenAsync(UserModel user);

    Task<bool> ValidateAccessTokenAsync(string token);

    Task<string> GenerateRefreshTokenAsync();

    Task<bool> ValidateRefreshToken(UserModel user);
}
