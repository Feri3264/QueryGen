using System;
using System.Text.Json.Nodes;

namespace QueryGen.Application.Common.Services;

public interface IPromptBuilder
{
    string GeneratePrompt(string Prompt , JsonArray Metadata);
}
