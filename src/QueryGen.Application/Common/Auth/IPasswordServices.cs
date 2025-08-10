using System;

namespace QueryGen.Application.Common.Auth;

public interface IPasswordServices
{
    string HashPassword(string Password);
}
