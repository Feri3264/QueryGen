namespace QueryGen.Application.Session.Command.Create;

public record CreateSessionCommand(
    string SessionName,
    string Server,
    string DbName,
    bool useWinAuth = false,
    string username = null,
    string password = null,
    int? port = null);