using System;
using System.Text.Json.Nodes;

namespace QueryGen.Application.Common.Services;

public interface IDbServices
{
    JsonArray GetMetadata(string ConnectionString);

    Task<string> ExecuteQuery(string query);
}
