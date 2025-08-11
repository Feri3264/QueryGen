using System;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Repository;

public interface IUserRepository
{
    Task<UserModel?> FindByUsername(string Username);

    Task<bool> IsUsernameExists(string Username);

    Task<bool> IsUserExists(Guid Id);

    Task AddAsync(UserModel model);

    Task SaveAsync();
}
