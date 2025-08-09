using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.Services;
using QueryGen.Application.Common.Utilities;

namespace QueryGen.Application.LLM.CompletePrompt;

public class CompletePromptHandler(
    ISessionServices sessionServices,
    ILlmServices llmServices,
    IDbServices dbServices) : IRequestHandler<CompletePromptCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CompletePromptCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionServices.GetById(request.SessionId);

        var llmResponse = await llmServices.GetCompletionAsync(request.prompt, session.Metadata);

        var query = QueryExtractor.ExtractSqlQuery(llmResponse);

        var result = await dbServices.ExecuteQuery(query);

        return result;
    }
}
