using System;
using ErrorOr;
using Microsoft.Data.SqlClient;
using QueryGen.Domain.Common.Enums;
using QueryGen.Domain.Session;

namespace QueryGen.Application.Common.Utilities.ConnectionString;

public class ConnectionStringBuilderFactory
{
    public static ErrorOr<IConnectionStringBuilder> Create(DatabaseTypeEnum DbType)
    {
        return DbType switch
        {
            DatabaseTypeEnum.sqlserver => new SqlServerConnectionBuilder(),
            DatabaseTypeEnum.postgresql => new PgSqlConnectionBuilder(),
            _ => SessionError.DbTypeNotValid
        };
    }
}
