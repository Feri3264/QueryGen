using System;

namespace QueryGen.Application.Common.DTOs.User;

public class LoginResult
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string RefreshToken { get; set; }

    public DateTime TokenExpire { get; set; }
}
