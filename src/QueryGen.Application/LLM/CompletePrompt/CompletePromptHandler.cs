using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Services;
using QueryGen.Application.Common.Utilities;

namespace QueryGen.Application.LLM.CompletePrompt;

public class CompletePromptHandler(
    ISessionServices sessionServices,
    ILlmServiceFactory llmFactory,
    IDbServices dbServices,
    IPromptBuilder promptBuilder) : IRequestHandler<CompletePromptCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CompletePromptCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.GetById(request.SessionId , request.UserId);

        if (session.IsError)
            return session.Errors;


        var prompt = promptBuilder.GeneratePrompt(request.prompt , session.Value.DbType , session.Value.Metadata);

        if (prompt.IsError)
            return prompt.Errors;

        var provider = llmFactory.GetProvider(session.Value.LlmType);
        var llmResponse = await provider.GetCompletionAsync(prompt.Value , session.Value.ApiToken , session.Value.LlmModel);

        if (llmResponse.IsError)
            return llmResponse.Errors;

        var query = QueryExtractor.ExtractSqlQuery(llmResponse.Value);

        if (query.IsError)
            return query.Errors;

        var isQueryValid = QueryValidator.IsValid(query.Value);

        if (isQueryValid.IsError)
            return isQueryValid.Errors;

        var result = await dbServices.ExecuteQuery(session.Value.ConnectionString , session.Value.DbType , query.Value);

        if (result.IsError)
            return result.Errors;

        var history = await sessionServices.CreateHistoryAsync(session.Value.Id , request.prompt , query.Value , result.Value);

        if (history.IsError)
            return history.Errors;

        return result.Value;
    }
}
