using GymProgresser.Application.Auth.Classes;
using GymProgresser.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Auth.Interfaces
{
    public interface IPasswordManager
    {
        public PasswordHashResult HashPassword(string plainPassword);
        public bool VerifyPassword(PasswordVerificationData passwordVerificationRequest);
    }
}
