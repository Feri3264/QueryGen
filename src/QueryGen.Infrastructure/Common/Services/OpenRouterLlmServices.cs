using System;
using System.Text.Json.Nodes;
using QueryGen.Application.Common.Services;
using System.Text.Json;
using System.Text;
using ErrorOr;

namespace QueryGen.Infrastructure.Common.Services;

public class OpenRouterLlmServices : ILlmServices
{
    public async Task<ErrorOr<string>> GetCompletionAsync(string Prompt , string ApiToken , string LlmModel)
    {
        var apiKey = ApiToken;
        var apiUrl = "https://openrouter.ai/api/v1/chat/completions";


        var client = new HttpClient();

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        client.DefaultRequestHeaders.Add("Accept", "application/json");


        var requestBody = new
        {
            model = LlmModel,
            messages = new[]
            {
                new { role = "user", content = Prompt }
            }
        };


        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await client.PostAsync(apiUrl, content);
        var responseContent = await response.Content.ReadAsStringAsync();


        if (!response.IsSuccessStatusCode)
            return Error.NotFound
                (code : response.StatusCode.ToString() , description : $"LLM Error : {response.ReasonPhrase}");


        using var doc = JsonDocument.Parse(responseContent);
        var root = doc.RootElement;


        var message = root
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();


        if (message is null)
            return Error.Validation
                (code: "message.is.null", description: "The Message Is Null");


        return message;
    }
}
