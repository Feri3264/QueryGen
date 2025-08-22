using System;
using ErrorOr;

namespace QueryGen.Application.Common.Utilities.ConnectionString;

public interface IConnectionStringBuilder
{
    ErrorOr<string> Build(
        string server,
        string database,
        string username,
        string password,
        int? port);
}
