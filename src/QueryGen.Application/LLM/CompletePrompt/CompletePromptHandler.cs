using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Services;
using QueryGen.Application.Common.Utilities;

namespace QueryGen.Application.LLM.CompletePrompt;

public class CompletePromptHandler(
    ISessionServices sessionServices,
    ILlmServices llmServices,
    IDbServices dbServices,
    IPromptBuilder promptBuilder) : IRequestHandler<CompletePromptCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CompletePromptCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.GetById(request.SessionId);

        var prompt = promptBuilder.GeneratePrompt(request.prompt , session.Value.Metadata);

        var llmResponse = await llmServices.GetCompletionAsync(prompt);

        var query = QueryExtractor.ExtractSqlQuery(llmResponse.Value);

        var isQueryValid = QueryValidator.IsValid(query.Value);

        var result = await dbServices.ExecuteQuery(session.Value.ConnectionString , query.Value);

        return result;
    }
}
