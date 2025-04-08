using GymProgresser.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateOnly BirthDate { get; set; }
        public ProfileGender Gender { get; set; }
    }

}
