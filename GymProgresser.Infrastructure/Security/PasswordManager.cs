using GymProgresser.Application.Auth.Classes;
using GymProgresser.Application.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Infrastructure.Security
{
    public class PasswordManager : IPasswordManager
    {
        const int ITERATIONS = 100_000;
        public PasswordHashResult HashPassword(string plainPassword)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16); 

            var pbkdf2 = new Rfc2898DeriveBytes(
                password: plainPassword,
                salt: salt,
                iterations: ITERATIONS,
                hashAlgorithm: HashAlgorithmName.SHA256
            );

            byte[] hash = pbkdf2.GetBytes(32); 

            var result = new PasswordHashResult
            {
                Hash = Convert.ToHexString(hash),
                Salt = Convert.ToHexString(salt)
            };

            return result;
        }

        public bool VerifyPassword(PasswordVerificationData passwordVerificationRequest)
        {
            var saltBytes = Convert.FromHexString(passwordVerificationRequest.Salt);

            var pbkdf2 = new Rfc2898DeriveBytes(
                password: passwordVerificationRequest.PlainPassword,
                salt: saltBytes,
                iterations: ITERATIONS,
                hashAlgorithm: HashAlgorithmName.SHA256
            );

            var hashBytes = pbkdf2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(
                Convert.FromHexString(passwordVerificationRequest.PasswordHash),
                hashBytes);
        }
    }
}
