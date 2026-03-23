using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    internal class PasswordService
    {
        private const int Iteration = 150_000;
        private const int SaltSize = 16;
        private const int KeySize = 32;

        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iteration, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(key);
        }

        public static bool Verify(string stored, string password)
        {
            var parts = stored.Split('.');
            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iteration, HashAlgorithmName.SHA256);
            var testKey = pbkdf2.GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(key, testKey);
        }
    }
}
