using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QueryGen.Application.Common.Auth;
using QueryGen.Domain.User;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace QueryGen.Infrastructure.Common.Auth;

public class JwtAuthServices
    (IConfiguration configuration) : IAuthServices
{

    private string TokenKey = configuration["Jwt:Key"];
    private static TimeSpan TokenExpiry = TimeSpan.FromHours(1);


    public string GenerateAccessTokenAsync(UserModel user)
    {
        var key = Encoding.UTF8.GetBytes(TokenKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.Add(TokenExpiry),
            signingCredentials: credentials,
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"]);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshTokenAsync()
    {
        var bytes = new byte[64];
        var numbers = RandomNumberGenerator.Create();
        numbers.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public bool ValidateRefreshToken(UserModel user)
    {
        if (user.TokenExpire < DateTime.Now)
            return false;

        return true;
    }
}
