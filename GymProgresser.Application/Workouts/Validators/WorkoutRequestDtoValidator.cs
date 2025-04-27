using FluentValidation;
using GymProgresser.Application.Workouts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Validators
{
    public class WorkoutRequestDtoValidator : AbstractValidator<WorkoutRequestDto>
    {
        public WorkoutRequestDtoValidator() 
        {
            RuleFor(d => d.DurationMin)
                .GreaterThan(0)
                .WithMessage("Trening musi trwać dłużej niż 0 minut");
        }
    }
}
