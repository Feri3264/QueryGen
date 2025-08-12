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

        if (session.IsError)
            return session.Errors;

        var prompt = promptBuilder.GeneratePrompt(request.prompt , session.Value.Metadata);

        if (prompt.IsError)
            return prompt.Errors;

        var llmResponse = await llmServices.GetCompletionAsync(prompt.Value , session.Value.ApiToken);

        if (llmResponse.IsError)
            return llmResponse.Errors;

        var query = QueryExtractor.ExtractSqlQuery(llmResponse.Value);

        if (query.IsError)
            return query.Errors;

        var isQueryValid = QueryValidator.IsValid(query.Value);

        if (isQueryValid.IsError)
            return isQueryValid.Errors;

        var result = await dbServices.ExecuteQuery(session.Value.ConnectionString , query.Value);

        if (result.IsError)
            return result.Errors;

        return result.Value;
    }
}
