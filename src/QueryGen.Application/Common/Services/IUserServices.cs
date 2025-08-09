using System;
using QueryGen.Domain.User;

namespace QueryGen.Application.Common.Services;

public interface IUserServices
{
    Task<UserModel> LoginAsync(string username , string password);
}
