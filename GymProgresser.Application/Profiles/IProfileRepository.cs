using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Profiles
{
    public interface IProfileRepository
    {
        public Task<Profile?> GetProfileByUserIdAsync(int userId); 
    }
}
