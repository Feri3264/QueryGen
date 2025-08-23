using System;
using System.Text;
using System.Text.Json;
using ErrorOr;
using QueryGen.Application.Common.Services;

namespace QueryGen.Infrastructure.Common.Services;

public class OllamaLlmServices : ILlmServices
{
    public async Task<ErrorOr<string>> GetCompletionAsync(string Prompt, string? ApiToken, string LlmModel)
    {
        var baseUrl = "http://localhost:11434/api/generate";

        var client = new HttpClient();

        var requestBody = new
        {
            model = LlmModel,
            prompt = Prompt,
            Stream = false    
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(baseUrl, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return Error.NotFound(
                code: response.StatusCode.ToString(), description: $"LLM Error : {response.ReasonPhrase}");

        using var doc = JsonDocument.Parse(responseContent);
        var result = doc.RootElement.GetProperty("response").GetString();

        if (result is null)
            return Error.Validation
                (code : "response.is.null" , description : "LLM Response Is Null");

        return result;
    }
}
