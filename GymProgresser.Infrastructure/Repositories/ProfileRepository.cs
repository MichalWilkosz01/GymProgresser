using GymProgresser.Application.Profiles;
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
    public class ProfileRepository : IProfileRepository
    {
        private readonly GymProgressDbContext _dbContext;
        public ProfileRepository(GymProgressDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Profile?> GetProfileByUserIdAsync(int userId)
        {
            var user = _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            return user;
        }
    }
}
