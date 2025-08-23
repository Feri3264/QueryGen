using System;
using System.Text.Json.Nodes;
using ErrorOr;

namespace QueryGen.Application.Common.Services;

public interface ILlmServices
{
    Task<ErrorOr<string>> GetCompletionAsync(string Prompt , string? ApiToken , string LlmModel);
}
