namespace QueryGen.Contracts.DTOs.User.Login;

public record LoginRequestDto(
    string Username,
    string Password
);