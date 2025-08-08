using System;
using QueryGen.Domain.Common;
using ErrorOr;

namespace QueryGen.Domain.User;

public class UserModel : BaseClass
{
    public string Username { get; private set; }

    public string Password { get; private set; }

    public string RefreshToken { get; private set; }

    public DateTime TokenExpire { get; private set; }



    //navigation



    //ctor
    private UserModel(
        string _username,
        string _password,
        string _refreshToken,
        DateTime _tokenExpire)
    {
        Id = Guid.NewGuid();
        Username = _username;
        Password = _password;
        RefreshToken = _refreshToken;
        TokenExpire = _tokenExpire;
    }



    //methods
    public static ErrorOr<UserModel> Create(
        string _username,
        string _password,
        string _refreshToekn,
        DateTime _tokenExpire)
    {
        //password validation
        var validatePassword = ValidatePassword(_password);
        if (validatePassword.IsError)
            return validatePassword.Errors;

        //name validation
        if (string.IsNullOrWhiteSpace(_username))
            return UserError.UserNotFound;

        return new UserModel(_username, _password, _refreshToekn, _tokenExpire);
    }


    public void SetRefreshToken(string value)
    {
        RefreshToken = value;
    }


    public void SetTokenExpireTime(DateTime date)
    {
        TokenExpire = date;
    }


    #region Password Validation
    private static ErrorOr<Success> ValidatePassword(string password)
    {
        if (password.Length <= 8)
        {
            return UserError.PasswordEightChar;
        }

        if (!password.Any(c => IsLetter(c)))
        {
            return UserError.PasswordContainLetter;
        }

        if (!password.Any(c => IsDeigit(c)))
        {
            return UserError.PasswordContainNumber;
        }

        return Result.Success;
    }
    private static bool IsLetter(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

    private static bool IsDeigit(char c)
    {
        return (c >= '0' && c <= '9');
    }
    #endregion


    private UserModel() { }
}
