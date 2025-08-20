using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Services;
using QueryGen.Application.Common.Utilities;

namespace QueryGen.Application.LLM.QueryPreview;

public class QueryPreviewHandler(
    ISessionServices sessionServices,
    ILlmServices llmServices,
    IPromptBuilder promptBuilder) : IRequestHandler<QueryPreviewCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(QueryPreviewCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.GetById(request.SessionId, request.UserId);

        if (session.IsError)
            return session.Errors;

        var prompt = promptBuilder.GeneratePrompt(request.prompt, session.Value.Metadata);

        if (prompt.IsError)
            return prompt.Errors;

        var llmResponse = await llmServices.GetCompletionAsync(prompt.Value, session.Value.ApiToken, session.Value.LlmModel);

        if (llmResponse.IsError)
            return llmResponse.Errors;

        var query = QueryExtractor.ExtractSqlQuery(llmResponse.Value);

        if (query.IsError)
            return query.Errors;

        var isQueryValid = QueryValidator.IsValid(query.Value);

        if (isQueryValid.IsError)
            return isQueryValid.Errors;

        return query;
    }
}
