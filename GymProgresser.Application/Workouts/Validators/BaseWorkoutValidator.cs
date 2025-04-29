using FluentValidation;
using GymProgresser.Application.Workouts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Validators
{
    public abstract class BaseWorkoutValidator<T> : AbstractValidator<T> where T : class
    {
        public BaseWorkoutValidator()
        {
            RuleFor(d => (double)d.GetType().GetProperty("DurationMin")!.GetValue(d)!)
                .GreaterThan(0)
                .WithMessage("Trening musi trwać dłużej niż 0 minut");


        }
    }
}
