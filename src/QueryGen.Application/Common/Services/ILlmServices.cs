using System;
using System.Text.Json.Nodes;

namespace QueryGen.Application.Common.Services;

public interface ILlmServices
{
    Task<string> GetCompletionAsync(string prompt , JsonArray Metadata);
}
