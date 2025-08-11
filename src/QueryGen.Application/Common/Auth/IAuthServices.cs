using System;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Auth;

public interface IAuthServices
{
    string GenerateAccessTokenAsync(UserModel user);

    string GenerateRefreshTokenAsync();

    bool ValidateRefreshToken(UserModel user);
}
