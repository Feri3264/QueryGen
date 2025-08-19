namespace QueryGen.Contracts.DTOs.Session.GetHistory;

public record GetHistoryResponseDto(
    Guid Id,
    string Prompt,
    string Query,
    string Result,
    DateTime CreatedAt,
    Guid SessionId
);
