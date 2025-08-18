namespace QueryGen.Contracts.DTOs.Session.ChangeLlmModel;

public record ChangeLlmModelResponseDto(
    string Name,
    string ConnectionString,
    string Metadata,
    string ApiToken,
    string LlmModel,
    Guid UserId
);