using System;
using System.Data;
using System.Text.Json;
using ErrorOr;
using Microsoft.Data.SqlClient;
using QueryGen.Application.Common.Services;
using Newtonsoft.Json;

namespace QueryGen.Infrastructure.Common.Services;

public class DbServices : IDbServices
{
    public async Task<ErrorOr<string>> ExecuteQuery(string connectionString, string query)
    {
        var testConnection = TestConnection(connectionString);

        if (testConnection.IsError)
            return testConnection.Errors;

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(query, connection);
        await connection.OpenAsync();

        var dataTable = new DataTable();
        using var reader = await command.ExecuteReaderAsync();
        dataTable.Load(reader);

        string result = JsonConvert.SerializeObject(dataTable, Formatting.Indented);

        return result;
    }


    public async Task<ErrorOr<string>> GetMetadata(string ConnectionString)
    {
        var testConnection = TestConnection(ConnectionString);

        if (testConnection.IsError)
            return testConnection.Errors;

 
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

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

        // JSON خروجی فشرده
        string json = JsonConvert.SerializeObject(metadata, Formatting.Indented);

        return json;
    }



    //Utilities
    private ErrorOr<bool> TestConnection(string connectionString)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.State == ConnectionState.Open;
            }
        }
        catch (Exception ex)
        {
            return Error.Validation
                (code: ex.Message, description: "Couldn't Connect To Database.");
        }
    }

    private List<Dictionary<string, object>> DataTableToList(DataTable table)
    {
        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        return list;
    }
}
