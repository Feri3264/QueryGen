namespace QueryGen.Contracts.DTOs.Session.GetSessionHistories;

public record GetSessionHistoriesResponseDto(
    Guid Id,
    string Prompt,
    string Query,
    string Result,
    DateTime CreatedAt,
    Guid SessionId
);
