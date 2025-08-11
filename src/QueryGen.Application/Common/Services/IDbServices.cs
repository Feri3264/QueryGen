using System;
using System.Data;
using System.Text.Json.Nodes;
using ErrorOr;

namespace QueryGen.Application.Common.Services;

public interface IDbServices
{
    Task<ErrorOr<string>> GetMetadata(string ConnectionString);

    Task<ErrorOr<string>> ExecuteQuery(string connectionString, string query);
}
