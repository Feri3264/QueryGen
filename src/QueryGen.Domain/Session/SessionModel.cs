using System;
using System.Text.Json.Nodes;
using ErrorOr;
using QueryGen.Domain.Common;

namespace QueryGen.Domain.Session;

public class SessionModel : BaseClass
{
    public string Name { get; set; }

    public string ConnectionString { get; set; }

    public string Metadata { get; set; }

    public string ApiToken { get; set; }


    //navigation
    public Guid UserId { get; set; }



    public SessionModel(
        string _name,
        string _connectionString,
        string _metadata,
        Guid _userId,
        string _apiToken
    )
    {
        Id = Guid.NewGuid();
        Name = _name;
        ConnectionString = _connectionString;
        Metadata = _metadata;
        UserId = _userId;
        ApiToken = _apiToken;
    }



    public static ErrorOr<SessionModel> Create(
        string _name,
        string _connectionString,
        string _metadata,
        Guid _userId,
        string _apiToken
    )
    {
        if (string.IsNullOrWhiteSpace(_name))
            return SessionError.NameIsNullOrEmpty;

        if (string.IsNullOrWhiteSpace(_connectionString))
            return SessionError.ConnStirngIsNullOrEmpty;

        if (string.IsNullOrWhiteSpace(_apiToken))
            return SessionError.ApiTokenIsNullOrEmpty;

        return new SessionModel(_name, _connectionString, _metadata, _userId , _apiToken);
    }



    private SessionModel() { }
}
