using FluentValidation;
using GymProgresser.Application.Workouts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Validators
{
    public class UpdateWorkoutRequestDtoValidator : BaseWorkoutValidator<UpdateWorkoutRequestDto>
    {
        public UpdateWorkoutRequestDtoValidator() : base()
        {
            RuleFor(i => i.Id)
                .NotNull()
                .WithMessage("Id treningu musi być podane do aktualizacji.");
        }
    }
}
