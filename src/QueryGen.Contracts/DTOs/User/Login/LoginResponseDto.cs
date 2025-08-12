namespace QueryGen.Contracts.DTOs.User.Login;

public record LoginResponseDto(
    Guid Id,
    string Username,
    string Password,
    string RefreshToken,
    DateTime TokenExpire,
    string JwtToken
);
