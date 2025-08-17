using System;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Repository;

public interface IUserRepository
{
    Task<UserModel?> GetById(Guid Id);

    Task<UserModel?> FindByUsername(string Username);

    Task<UserModel?> GetByRefreshToken(string token);

    Task<bool> IsUsernameExists(string Username);

    Task<bool> IsUserExists(Guid Id);

    Task AddAsync(UserModel model);

    void Update(UserModel model);

    Task SaveAsync();
}
