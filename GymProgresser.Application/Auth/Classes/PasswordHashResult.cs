using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Infrastructure.Security
{
    public class PasswordHashResult
    {
        public string Salt { get; set; } = default!;
        public string Hash { get; set; } = default!;
    }
}
