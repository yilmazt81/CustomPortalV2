using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Helper
{
    public static class TokenHasher
    {
        public static string Hash(string token)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }
    }
}
