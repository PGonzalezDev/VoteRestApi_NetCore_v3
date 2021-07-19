﻿using System.Security.Cryptography;
using System.Text;

namespace VotesRestApi.Core.Helper
{
    public static class CryptographyHelper
    {
        public static string Encrypt(string value)
        {
            byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value));
            
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
    }
}
