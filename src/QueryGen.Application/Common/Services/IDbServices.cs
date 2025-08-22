using System;
using System.Data;
using System.Text.Json.Nodes;
using ErrorOr;
using QueryGen.Domain.Common.Enums;

namespace QueryGen.Application.Common.Services;

public interface IDbServices
{
    Task<ErrorOr<string>> GetMetadata(string ConnectionString ,DatabaseTypeEnum dbType);

    Task<ErrorOr<string>> ExecuteQuery(string connectionString, DatabaseTypeEnum dbType , string query);
}
