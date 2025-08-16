namespace QueryGen.Contracts.DTOs.Session.CreateSession;

public record CreateSessionResponseDto(
    string Name,
    string ConnectionString,
    string Metadata,
    string ApiToken,
    string LlmModel,
    Guid UserId
);
