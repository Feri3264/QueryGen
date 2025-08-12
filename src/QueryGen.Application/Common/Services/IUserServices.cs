using System;
using ErrorOr;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Services;

public interface IUserServices
{
    Task<bool> IsUserExists(Guid Id);

    Task<ErrorOr<UserModel>> LoginAsync(string username, string password);

    Task<ErrorOr<UserModel>> RegisterAsync(string username, string password);

    Task<ErrorOr<UserModel>> GetByRefreshToken(string RefreshToken);
}
