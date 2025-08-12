using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.User;

namespace QueryGen.Application.User.Command.RefreshToken;

public record ValidateRefreshTokenCommand(string RefreshToken) : IRequest<ErrorOr<RefreshTokenResult>>;
