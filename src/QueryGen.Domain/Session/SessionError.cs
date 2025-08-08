using System;
using ErrorOr;

namespace QueryGen.Domain.Session;

public class SessionError
{
    public static Error SessionNotFound = Error.NotFound
        (code : "session.not.found" , description : "Session Not Found");
    public static Error NameIsNullOrEmpty = Error.Validation
        (code: "name.is.nullOrEmpty", description: "Name Is Null Or Empty");

    public static Error ConnStirngIsNullOrEmpty = Error.Validation
        (code: "connString.is.nullOrEmpty", description: "ConnectionString Is Null Or Empty");
}
