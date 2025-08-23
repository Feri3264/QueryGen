using System;
using System.Text.Json.Nodes;
using ErrorOr;
using QueryGen.Domain.Common;
using QueryGen.Domain.Common.Enums;

namespace QueryGen.Domain.Session;

public class SessionModel : BaseClass
{
    public string Name { get; private set; }

    public string ConnectionString { get; private set; }

    public string Metadata { get; private set; }

    public string? ApiToken { get; private set; }

    public string LlmModel { get; private set; }

    public DatabaseTypeEnum DbType { get; set; }

    public LlmTypeEnum LlmType { get; set; }


    //navigation
    public Guid UserId { get; private set; }



    public SessionModel(
        string _name,
        string _connectionString,
        string _metadata,
        Guid _userId,
        string? _apiToken,
        string _llmModel,
        DatabaseTypeEnum _dbType,
        LlmTypeEnum _llmType
    )
    {
        Id = Guid.NewGuid();
        Name = _name;
        ConnectionString = _connectionString;
        Metadata = _metadata;
        UserId = _userId;
        ApiToken = _apiToken;
        LlmModel = _llmModel;
        DbType = _dbType;
        LlmType = _llmType;
    }



    public static ErrorOr<SessionModel> Create(
        string _name,
        string _connectionString,
        string _metadata,
        Guid _userId,
        string? _apiToken,
        string _llmModel,
        DatabaseTypeEnum _dbType,
        LlmTypeEnum _llmType
    )
    {
        if (string.IsNullOrWhiteSpace(_name))
            return SessionError.NameIsNullOrEmpty;

        if (string.IsNullOrWhiteSpace(_connectionString))
            return SessionError.ConnStirngIsNullOrEmpty;

        return new SessionModel(_name, _connectionString, _metadata, _userId, _apiToken, _llmModel , _dbType , _llmType);
    }

    public void SetName(string value)
    {
        Name = value;
    }

    public void SetModel(string value)
    {
        LlmModel = value;
    }

    private SessionModel() { }
}
