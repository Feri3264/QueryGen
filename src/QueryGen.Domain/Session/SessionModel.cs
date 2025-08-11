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


    //navigation
    public Guid UserId { get; set; }



    public SessionModel(
        string _name,
        string _connectionString,
        string _metadata,
        Guid _userId
    )
    {
        Id = Guid.NewGuid();
        Name = _name;
        ConnectionString = _connectionString;
        Metadata = _metadata;
        UserId = _userId;
    }



    public static ErrorOr<SessionModel> Create(
        string _name,
        string _connectionString,
        string _metadata,
        Guid _userId
    )
    {
        if (string.IsNullOrWhiteSpace(_name))
            return SessionError.NameIsNullOrEmpty;

        if (string.IsNullOrWhiteSpace(_connectionString))
            return SessionError.ConnStirngIsNullOrEmpty;

        return new SessionModel(_name, _connectionString, _metadata, _userId);
    }



    private SessionModel() { }
}
