namespace QueryGen.Contracts.DTOs.User.RefreshToken;

public record RefreshTokenResponseDto(
    string JwtToken,
    string RefreshToken
);