namespace QueryGen.Contracts.DTOs.Session.GetSession;

public record GetSessionResponseDto(
    string Name,
    string ConnectionString,
    string Metadata,
    string ApiToken,
    Guid UserId
);
