using System;
using System.Text.Json.Nodes;
using ErrorOr;
using QueryGen.Domain.Common;

namespace QueryGen.Domain.Session;

public class SessionModel : BaseClass
{
    public string Name { get; private set; }

    public string ConnectionString { get; private set; }

    public string Metadata { get; private set; }

    public string ApiToken { get; private set; }

    public string LlmModel { get; private set; }


    //navigation
    public Guid UserId { get; private set; }



    public SessionModel(
        string _name,
        string _connectionString,
        string _metadata,
        Guid _userId,
        string _apiToken,
        string _llmModel
    )
    {
        Id = Guid.NewGuid();
        Name = _name;
        ConnectionString = _connectionString;
        Metadata = _metadata;
        UserId = _userId;
        ApiToken = _apiToken;
        LlmModel = _llmModel;
    }



    public static ErrorOr<SessionModel> Create(
        string _name,
        string _connectionString,
        string _metadata,
        Guid _userId,
        string _apiToken,
        string _llmModel
    )
    {
        if (string.IsNullOrWhiteSpace(_name))
            return SessionError.NameIsNullOrEmpty;

        if (string.IsNullOrWhiteSpace(_connectionString))
            return SessionError.ConnStirngIsNullOrEmpty;

        if (string.IsNullOrWhiteSpace(_apiToken))
            return SessionError.ApiTokenIsNullOrEmpty;

        return new SessionModel(_name, _connectionString, _metadata, _userId , _apiToken , _llmModel);
    }

    public void SetName(string value)
    {
        Name = value;
    }

    private SessionModel() { }
}
