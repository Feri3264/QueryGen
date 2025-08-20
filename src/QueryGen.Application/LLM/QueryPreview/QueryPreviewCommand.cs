using MediatR;
using ErrorOr;

namespace QueryGen.Application.LLM.QueryPreview;

public record QueryPreviewCommand(Guid SessionId, Guid UserId, string prompt) : IRequest<ErrorOr<string>>;
