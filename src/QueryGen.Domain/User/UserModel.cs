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

    public void SetPassword(string value)
    {
        Password = value;    
    }


    private UserModel() { }
}
