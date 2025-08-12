using System;
using ErrorOr;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Auth;

public interface IAuthServices
{
    string GenerateAccessTokenAsync(UserModel user);

    string GenerateRefreshTokenAsync();

    ErrorOr<Success> ValidateRefreshToken(UserModel user);
}
