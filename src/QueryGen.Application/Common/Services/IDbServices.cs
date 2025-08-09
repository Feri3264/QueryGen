using System;
using System.Text.Json.Nodes;

namespace QueryGen.Application.Common.Services;

public interface IDbServices
{
    string BuildConnectionString
        (string Server, string DbName, bool useWinAuth = false, string username = null, string password = null, int? port = null);

    JsonArray GetMetadata(string ConnectionString);
}
