using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Auth.Dtos
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = default!;
    }
}
