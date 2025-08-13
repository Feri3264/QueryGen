using System;
using ErrorOr;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace QueryGen.Application.Common.Utilities;

public static class QueryValidator
{
    private static readonly string[] AllowedCommands = { "SELECT" };


    private static readonly string[] ForbiddenKeywords =
    {
        "INSERT", "UPDATE", "DELETE", "DROP", "ALTER", "TRUNCATE", "EXEC", "MERGE"
    };


    // private static readonly Regex SelectRegex = new(@"^SELECT\s+.+\s+FROM\s+\w+", RegexOptions.IgnoreCase | RegexOptions.Compiled);


    public static ErrorOr<Success> IsValid(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Error.Validation
                (code : "query.is.empty" , description : "The Query Is Empty");
        }


        string upperQuery = query.Trim().ToUpperInvariant();


        if (!AllowedCommands.Any(cmd => upperQuery.StartsWith(cmd)))
        {
            return Error.Validation
                (code : "query.not.select" , description : "Only SELECT queries are allowed.");
        }


        if (ForbiddenKeywords.Any(k => upperQuery.Contains(k)))
        {
            return Error.Validation
                (code : "query.contains.forbiddenWords" , description : "Query contains forbidden keywords.");
        }


        // if (!SelectRegex.IsMatch(query))
        // {
        //     return Error.Validation
        //         (code : "syntax.not.valid" , description : "Query syntax is invalid or not a simple SELECT.");
        // }

        return Result.Success;
    }
}
