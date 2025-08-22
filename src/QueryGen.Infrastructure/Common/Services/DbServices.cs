using System;
using System.Data;
using System.Text.Json;
using ErrorOr;
using Microsoft.Data.SqlClient;
using QueryGen.Application.Common.Services;
using Newtonsoft.Json;
using System.Data.Common;
using Npgsql;
using QueryGen.Domain.Common.Enums;
using QueryGen.Application.Common.Utilities;
using System.Threading.Tasks;

namespace QueryGen.Infrastructure.Common.Services;

public class DbServices : IDbServices
{
    public async Task<ErrorOr<string>> ExecuteQuery(string connectionString, DatabaseTypeEnum dbType, string query)
    {
        var testConnection = await TestConnection(connectionString, dbType);

        if (testConnection.IsError)
            return testConnection.Errors;

        using var connection = DbConnectionFactory.CreateConnection(dbType, connectionString);

        await connection.OpenAsync();

        using var command = connection.CreateCommand();

        var dataTable = new DataTable();
        using var reader = await command.ExecuteReaderAsync();
        dataTable.Load(reader);

        string result = JsonConvert.SerializeObject(dataTable, Formatting.Indented);

        connection.Close();

        return result;
    }


    public async Task<ErrorOr<string>> GetMetadata(string ConnectionString, DatabaseTypeEnum dbType)
    {
        var testConnection = await TestConnection(ConnectionString, dbType);

        if (testConnection.IsError)
            return testConnection.Errors;

        using var connection = DbConnectionFactory.CreateConnection(dbType, ConnectionString);

        await connection.OpenAsync();


        var metadata = new Dictionary<string, List<Dictionary<string, string>>>();

        DataTable tables = connection.GetSchema("Tables");

        foreach (DataRow tableRow in tables.Rows)
        {
            string tableName = tableRow["TABLE_NAME"].ToString();

            DataTable columns = connection.GetSchema("Columns", new string[] { null, null, tableName });

            var columnList = new List<Dictionary<string, string>>();

            foreach (DataRow columnRow in columns.Rows)
            {
                columnList.Add(new Dictionary<string, string>
                {
                    ["name"] = columnRow["COLUMN_NAME"].ToString(),
                    ["type"] = columnRow["DATA_TYPE"].ToString()
                });
            }

            metadata[tableName] = columnList;
        }


        string json = JsonConvert.SerializeObject(metadata, Formatting.Indented);

        return json;
    }



    //Utilities
    private async Task<ErrorOr<bool>> TestConnection(string connectionString, DatabaseTypeEnum dbType)
    {
        try
        {
            using var connection = DbConnectionFactory.CreateConnection(dbType, connectionString);
            await connection.OpenAsync();
            return connection.State == ConnectionState.Open;
        }
        catch (Exception ex)
        {
            return Error.Validation(
                code: "db.connection.failed", description: $"Couldn't connect to database: {ex.Message}");
        }
    }
}
