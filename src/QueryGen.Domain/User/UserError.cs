using System;
using ErrorOr;

namespace QueryGen.Domain.User;

public class UserError
{
    public static Error UserNotFound = Error.NotFound
            (code: "user.not.found", description: "User Not Found");

    public static Error UsernameAlreadyExists = Error.Validation
        (code: "username.already.exists", description: "Username Already Taken");

    public static Error PasswordEightChar = Error.Validation
        (code: "password.8.char", description: "Password must be at least 8 characters");

    public static Error PasswordContainLetter = Error.Validation
        (code: "password.contain.letter", description: "Password must contain at least one letter");

    public static Error PasswordContainNumber = Error.Validation
        (code: "password.contain.number", description: "Password must contain at least one number");

    public static Error UsernameOrPasswordNotCorrect = Error.Validation
        (code: "usernameOrPassword.not.correct", description: "Username Or Password Is Not Correct");

    public static Error PasswordNotCorrect = Error.Validation
        (code: "Password.not.correct", description: "Password Is Incorrect");

    public static Error RefreshTokenNotFound = Error.Validation
        (code: "refreshToken.not.found", description: "User With The RefreshToken Not Found");
}
