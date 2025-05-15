using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Users.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<int> AddUserAsync(User user);
        public Task<User?> GetUserByIdAsync(int userId);
    }
}
