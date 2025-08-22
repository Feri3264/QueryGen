using System;
using ErrorOr;
using Npgsql;

namespace QueryGen.Application.Common.Utilities.ConnectionString;

public class PgSqlConnectionBuilder : IConnectionStringBuilder
{
    public ErrorOr<string> Build(
        string Server,
        string DbName,
        string username,
        string password,
        int? port = 5432
    )
    { 
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = Server,
            Port = Convert.ToInt32(port),
            Database = DbName,
            Username = username,
            Password = password,
            SslMode = SslMode.Prefer
        };

        return builder.ConnectionString;
    }
}
