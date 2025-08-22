using System;
using ErrorOr;
using Microsoft.Data.SqlClient;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Common.Utilities.ConnectionString;

public class SqlServerConnectionBuilder : IConnectionStringBuilder
{
    public ErrorOr<string> Build(
        string Server,
        string DbName,
        string? username = null,
        string? password = null,
        int? port = null
    )
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = port.HasValue ? $"{Server},{port}" : Server,
            InitialCatalog = DbName
        };

        if (username is null && password is null)
        {
            builder.IntegratedSecurity = true;
        }
        else
        {
            builder.UserID = username;
            builder.Password = password;
        }

        builder.TrustServerCertificate = true;
        builder.ConnectTimeout = 30;

        return builder.ToString();
    }
}
