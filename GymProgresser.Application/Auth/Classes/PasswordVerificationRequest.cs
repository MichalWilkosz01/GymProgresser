using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Auth.Classes
{
    public class PasswordVerificationRequest
    {
        public string PlainPassword { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Salt { get; set; } = default!;

        public PasswordVerificationRequest(User user, string plainPassword)
        {
            this.PlainPassword = plainPassword;
            this.PasswordHash = user.PasswordHash;
            this.Salt = user.Salt;
        }
    }
}
