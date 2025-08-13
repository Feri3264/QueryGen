using MediatR;
using ErrorOr;

namespace QueryGen.Application.LLM.CompletePrompt;

public record CompletePromptCommand(Guid SessionId, Guid UserId, string prompt) : IRequest<ErrorOr<string>>;