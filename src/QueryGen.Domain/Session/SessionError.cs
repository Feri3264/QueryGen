using System;
using ErrorOr;

namespace QueryGen.Domain.Session;

public class SessionError
{
    public static Error SessionNotFound = Error.NotFound
        (code: "session.not.found", description: "Session Not Found");
    public static Error NameIsNullOrEmpty = Error.Validation
        (code: "name.is.nullOrEmpty", description: "Name Is Null Or Empty");

    public static Error ConnStirngIsNullOrEmpty = Error.Validation
        (code: "connString.is.nullOrEmpty", description: "ConnectionString Is Null Or Empty");

    public static Error ConnStirngBuildFailed = Error.Validation
        (code: "connString.build.failed", description: "ConnectionString Building Process Failed");

    public static Error ApiTokenIsNullOrEmpty = Error.Validation
        (code: "apiToken.is.nullOrEmpty", description: "Api Token Is Null Or Empty");

    public static Error DbTypeIsNullOrEmpty = Error.Validation
        (code: "dbType.is.nullOrEmpty", description: "Database Type Is Null Or Empty");

    public static Error DbTypeNotValid = Error.Validation
        (code: "dbType.not.valid", description: "Database Type Is Not Valid");

    public static Error SessionThiefError = Error.Validation
        (code: "you.arent.theSessionOwner", description: "It Seems You Are Not The Owner Of This Session");
}
