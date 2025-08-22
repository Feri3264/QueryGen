namespace QueryGen.Contracts.DTOs.Session.CreateSession;

public record CreateSessionRequestDto(
    string SessionName,
    string ApiToken,
    string LlmModel,
    string DbType,
    string Server,
    string DbName,
    string username = null,
    string password = null,
    int? port = null
);
