using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Npgsql;
using QueryGen.Domain.Common.Enums;

namespace QueryGen.Application.Common.Utilities;

public static class DbConnectionFactory
{
    public static DbConnection CreateConnection(DatabaseTypeEnum dbType, string connectionString)
        {
            return dbType switch
            {
                DatabaseTypeEnum.sqlserver  => new SqlConnection(connectionString),
                DatabaseTypeEnum.postgresql => new NpgsqlConnection(connectionString),        
                _ => throw new NotSupportedException($"Database type {dbType} is not supported")
            };
        }
}
