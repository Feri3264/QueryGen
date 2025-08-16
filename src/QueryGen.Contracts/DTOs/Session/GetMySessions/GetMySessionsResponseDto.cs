namespace QueryGen.Contracts.DTOs.Session.GetMySessions;

public record GetMySessionsResponseDto(
    string Name,
    string ConnectionString,
    string Metadata,
    string ApiToken,
    string LlmModel,
    Guid UserId
);
