using System;
using ErrorOr;
using System.Text.Json.Nodes;
using QueryGen.Domain.Common.Enums;

namespace QueryGen.Application.Common.Services;

public interface IPromptBuilder
{
    ErrorOr<string> GeneratePrompt(string Prompt , DatabaseTypeEnum DbType , string Metadata);
}
