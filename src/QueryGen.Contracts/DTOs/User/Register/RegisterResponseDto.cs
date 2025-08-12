namespace QueryGen.Contracts.DTOs.User.Register;

public record RegisterResponseDto(
    Guid Id,
    string Username,
    string Password,
    string RefreshToken,
    DateTime TokenExpire,
    string JwtToken
);
