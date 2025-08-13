using System;
using ErrorOr;

namespace QueryGen.Application.Common.Auth;

public interface IPasswordServices
{
    string HashPassword(string Password);

    ErrorOr<Success> ValidatePassword(string Password);
}
