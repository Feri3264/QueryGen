using System;
using ErrorOr;
using System.Text.Json.Nodes;

namespace QueryGen.Application.Common.Services;

public interface IPromptBuilder
{
    ErrorOr<string> GeneratePrompt(string Prompt , string Metadata);
}
