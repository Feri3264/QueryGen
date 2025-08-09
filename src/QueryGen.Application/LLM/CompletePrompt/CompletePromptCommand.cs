using MediatR;
using ErrorOr;

namespace QueryGen.Application.LLM.CompletePrompt;

public record CompletePromptCommand(Guid SessionId, string prompt) : IRequest<ErrorOr<string>>;