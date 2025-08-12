using System;

namespace QueryGen.Application.Common.DTOs.User;

public class RefreshTokenResult
{
    public string JwtToken { get; set; }

    public string RefreshToken { get; set; }
}
