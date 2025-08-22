namespace QueryGen.Contracts.DTOs.Session.ChangeName;

public record ChangeNameResponseDto(
    string Name,
    string ConnectionString,
    string Metadata,
    string ApiToken,
    string LlmModel,
    string DbType,
    Guid UserId
);