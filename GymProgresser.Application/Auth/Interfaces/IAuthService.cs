using GymProgresser.Application.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        public Task<TokenResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        public Task<TokenResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
