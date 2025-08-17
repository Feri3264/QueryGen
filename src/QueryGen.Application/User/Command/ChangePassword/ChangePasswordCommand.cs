using MediatR;
using ErrorOr;
using QueryGen.Application.Common.DTOs.User;

namespace QueryGen.Application.User.Command.ChangePassword;

public record ChangePasswordCommand(
    Guid UserId,
    string newPassword,
    string oldPassword) : IRequest<ErrorOr<ChangePasswordResult>>;