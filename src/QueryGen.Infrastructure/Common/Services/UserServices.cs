using System;
using System.Threading.Tasks;
using ErrorOr;
using QueryGen.Application.Common.Auth;
using QueryGen.Application.Common.Repository;
using QueryGen.Application.Common.Services;
using QueryGen.Domain.User;

namespace QueryGen.Infrastructure.Common.Services;

public class UserServices(
    IUserRepository userRepository,
    IPasswordServices passwordServices,
    IAuthServices authServices) : IUserServices
{
    public async Task<ErrorOr<UserModel>> GetByRefreshToken(string RefreshToken)
    {
        var user = await userRepository.GetByRefreshToken(RefreshToken);

        if (user is null)
            return UserError.RefreshTokenNotFound;

        return user;
    }

    public async Task<bool> IsUserExists(Guid Id)
    {
        return await userRepository.IsUserExists(Id);
    }

    public async Task<ErrorOr<UserModel>> LoginAsync(string username, string password)
    {
        var user = await userRepository.FindByUsername(username);

        if (user is null)
            return UserError.UsernameOrPasswordNotCorrect;

        if (user.Password != passwordServices.HashPassword(password))
            return UserError.UsernameOrPasswordNotCorrect;

        var newRefreshToken = authServices.GenerateRefreshTokenAsync();

        user.SetRefreshToken(newRefreshToken);
        user.SetTokenExpireTime(DateTime.Now.AddDays(3));

        userRepository.Update(user);
        await userRepository.SaveAsync();

        return user;
    }

    public async Task<ErrorOr<UserModel>> RegisterAsync(string username, string password)
    {
        var validatePassword = passwordServices.ValidatePassword(password);

        if (validatePassword.IsError)
            return validatePassword.Errors;
            
        var user = UserModel.Create(
            username,
            passwordServices.HashPassword(password),
            authServices.GenerateRefreshTokenAsync(),
            DateTime.Now.AddDays(3)
        );

        if (user.IsError)
            return user.Errors;

        if (await userRepository.IsUsernameExists(username))
            return UserError.UsernameAlreadyExists;

        await userRepository.AddAsync(user.Value);
        await userRepository.SaveAsync();

        return user;
    }
}
