namespace QueryGen.Contracts.DTOs.User.ChangePassword;

public record ChangePasswordRequestDto(
    string newPassword,
    string oldPassword
);
