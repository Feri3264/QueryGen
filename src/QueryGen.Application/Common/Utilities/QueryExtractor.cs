using System;
using System.Text.RegularExpressions;
using ErrorOr;

namespace QueryGen.Application.Common.Utilities;

public static class QueryExtractor
{
    public static ErrorOr<string> ExtractSqlQuery(string value)
    {
        var match = Regex.Match(
            value,
            @"```sql\s*(.*?)\s*```|(SELECT|INSERT|UPDATE|DELETE)[\s\S]*?(?=\s*$)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline
        );

        if (match.Success)
        {
            // اگر حالت markdown باشه
            if (match.Groups[1].Success && !string.IsNullOrWhiteSpace(match.Groups[1].Value))
                return match.Groups[1].Value.Trim();

            // حالت کوئری مستقیم
            return match.Value.Trim();
        }

        return Error.Validation(
            code: "query.not.found",
            description: "SQL query not found."
        );
    }
}
