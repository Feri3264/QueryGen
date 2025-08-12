using System;
using QueryGen.Application.Common.Auth;
﻿using System.Security.Cryptography;
using System.Text;

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
}
