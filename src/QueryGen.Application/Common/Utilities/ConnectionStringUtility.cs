using System;
using ErrorOr;
using Microsoft.Data.SqlClient;
using QueryGen.Domain.Session;


namespace QueryGen.Application.Common.Utilities;

public static class ConnectionStringUtility
{
    public static ErrorOr<string> Build(
        string Server,
        string DbName,
        bool useWinAuth = false,
        string username = null,
        string password = null,
        int? port = null)
    {
        if (String.IsNullOrEmpty(Server) || String.IsNullOrEmpty(DbName))
            return SessionError.ConnStirngBuildFailed;

        var builder = new SqlConnectionStringBuilder
        {
            DataSource = port.HasValue ? $"{Server},{port}" : Server,
            InitialCatalog = DbName
        };

        if (useWinAuth)
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
