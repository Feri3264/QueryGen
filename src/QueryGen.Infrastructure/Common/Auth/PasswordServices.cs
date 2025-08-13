using System;
using QueryGen.Application.Common.Auth;
﻿using System.Security.Cryptography;
using System.Text;
using ErrorOr;
using QueryGen.Domain.User;

namespace QueryGen.Infrastructure.Common.Auth;

public class PasswordServices : IPasswordServices
{
    public string HashPassword(string Password)
    {
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;
        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(Password);
        encodedBytes = md5.ComputeHash(originalBytes);
        //Convert encoded bytes back to a 'readable' string   
        return BitConverter.ToString(encodedBytes);
    }

    public ErrorOr<Success> ValidatePassword(string Password)
    {
        if (Password.Length <= 8)
        {
            return UserError.PasswordEightChar;
        }

        if (!Password.Any(c => IsLetter(c)))
        {
            return UserError.PasswordContainLetter;
        }

        if (!Password.Any(c => IsDeigit(c)))
        {
            return UserError.PasswordContainNumber;
        }

        return Result.Success;
    }

    //Utilities
    private static bool IsLetter(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

    private static bool IsDeigit(char c)
    {
        return (c >= '0' && c <= '9');
    }
}
