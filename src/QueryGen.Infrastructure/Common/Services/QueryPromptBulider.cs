using System;
using QueryGen.Application.Common.Services;

namespace QueryGen.Infrastructure.Common.Services;

public class QueryPromptBulider : IPromptBuilder
{
    public string GeneratePrompt(string Prompt, string Metadata)
    {
        var systemInstructions = @"
        You are an SQL generator.
        - Do NOT provide explanations.
        - Return ONLY the SQL query without any additional text or formatting.
        - Use the provided database schema metadata to construct valid queries.
        ";

        var metadataInstruction = $"Database Metadata (in JSON format):\n{Metadata}";

        // ساخت پرامپت نهایی
        var finalPrompt = $"{systemInstructions}\n\n{metadataInstruction}\n\nUser Request:\n{Prompt}";

        return finalPrompt;
    }
}
