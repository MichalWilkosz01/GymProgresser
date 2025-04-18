using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Users
{
    public interface IUserService
    {
        public Task<string> RegisterUserAsync();
        public Task<string> LoginUserAsync();
    }
}
