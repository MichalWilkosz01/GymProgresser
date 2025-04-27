using GymProgresser.Application.Users;
using GymProgresser.Domain.Entities;
using GymProgresser.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GymProgressDbContext _dbContext;
        public UserRepository(GymProgressDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email.ToUpper());
        }

        public async Task<int> AddUserAsync(User user)
        {
            user.Email = user.Email.ToUpper();
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var res = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return res;
        }
    }
}
