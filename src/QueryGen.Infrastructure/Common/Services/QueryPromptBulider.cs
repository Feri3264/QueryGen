using System;
using ErrorOr;
using QueryGen.Domain.Common.Enums;
using QueryGen.Application.Common.Services;

namespace QueryGen.Infrastructure.Common.Services;

public class QueryPromptBulider : IPromptBuilder
{
    public ErrorOr<string> GeneratePrompt(string Prompt, DatabaseTypeEnum DbType , string Metadata)
    {
        if (String.IsNullOrEmpty(Prompt))
            return Error.Validation
                (code : "prompt.is.empty" , description : "Prompt Is Empty");

        var systemInstructions = @"
        You are an SQL generator.
        - Do NOT provide explanations.
        - Return ONLY the SQL query without any additional text or formatting.
        - Use the provided database schema metadata to construct valid queries.
        ";

        var databaseType = $"The database type is: {DbType}";

        var metadataInstruction = $"Database Metadata (in JSON format):\n{Metadata}";

        // ساخت پرامپت نهایی
        var finalPrompt = $"{systemInstructions}\n\n{databaseType}\n\n{metadataInstruction}\n\nUser Request:\n{Prompt}";

        return finalPrompt;
    }
}
