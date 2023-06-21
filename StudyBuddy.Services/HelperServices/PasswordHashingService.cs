using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using StudyBuddy.Domain.EncryptionInterfaces;
using System.Security.Cryptography;

namespace StudyBuddy.Services.Encryption
{
    public class PasswordHashingService : IPasswordHashingService
    {
        public string HashPassword(string password, byte[] salt)
        {
            var hash = KeyDerivation.Pbkdf2(password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 32);

            byte[] hashedPassword = new byte[48];
            Buffer.BlockCopy(salt, 0, hashedPassword, 0, 16);
            Buffer.BlockCopy(hash, 0, hashedPassword, 16, 32);

            return Convert.ToBase64String(hashedPassword);
        }

        public bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            Buffer.BlockCopy(hashedPasswordBytes, 0, salt, 0, 16);

            var hash = KeyDerivation.Pbkdf2(password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 32);

            var hashBytes = new byte[32];
            Buffer.BlockCopy(hashedPasswordBytes, 16, hashBytes, 0, 32);

            return hash.SequenceEqual<byte>(hashBytes);
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
