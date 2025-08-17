using System;
using ErrorOr;
using MediatR;
using QueryGen.Application.Common.DTOs.User;
using QueryGen.Application.Common.Mappers.User;
using QueryGen.Application.Common.Services;

namespace QueryGen.Application.User.Command.ChangePassword;

public class ChangePasswordHandler
    (IUserServices userServices) : IRequestHandler<ChangePasswordCommand, ErrorOr<ChangePasswordResult>>
{
    public async Task<ErrorOr<ChangePasswordResult>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userServices.ChangePassword(request.UserId, request.oldPassword, request.newPassword);

        if (user.IsError)
            return user.Errors;

        return UserMapper.ToChangePasswordResult(user.Value);
    }
}
