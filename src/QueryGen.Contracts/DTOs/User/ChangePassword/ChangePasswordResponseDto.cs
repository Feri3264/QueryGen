namespace QueryGen.Contracts.DTOs.User.ChangePassword;

public record ChangePasswordResponseDto(
    Guid Id,
    string Username,
    string Password,
    string RefreshToken,
    DateTime TokenExpire
);
