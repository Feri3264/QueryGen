using System;
using System.Text.RegularExpressions;
using ErrorOr;

namespace QueryGen.Application.Common.Utilities;

public static class QueryExtractor
{
    public static ErrorOr<string> ExtractSqlQuery(string value)
    {
        var match = Regex.Match(value, @"```sql\s*(.*?)\s*```", RegexOptions.Singleline);

        if (match.Success)
            return match.Groups[1].Value.Trim();


        match = Regex.Match(value, @"(SELECT|INSERT|UPDATE|DELETE)[\s\S]+?;", RegexOptions.IgnoreCase);

        if (match.Success)
            return match.Value.Trim();

        return Error.Validation
            (code : "query.not.found" , description : "SQL query not found.");
    }
}
