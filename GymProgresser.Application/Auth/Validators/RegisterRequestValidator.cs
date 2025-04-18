using FluentValidation;
using GymProgresser.Application.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Auth.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator() 
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("111111111111111111111111111111");
        }

    }
}
