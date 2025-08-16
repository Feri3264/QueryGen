namespace QueryGen.Contracts.DTOs.Session.CreateSession;

public record CreateSessionRequestDto(
    string SessionName,
    string ApiToken,
    string LlmModel,
    string Server,
    string DbName,
    bool useWinAuth = false,
    string username = null,
    string password = null,
    int? port = null
);
