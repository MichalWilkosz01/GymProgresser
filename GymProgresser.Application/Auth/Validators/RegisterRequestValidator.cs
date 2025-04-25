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
                .EmailAddress()
                .NotEmpty();

            RuleFor(p => p.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Must(password =>
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsLower) &&
                    password.Any(char.IsDigit) &&
                    password.Any(ch => "!@#$%^&*()-_=+[]{}|;:'\",.<>?/`~".Contains(ch))
                )
                .WithMessage("Hasło musi zawierać przynajmniej jedną wielką i małą literę oraz znak specjalny!");

            RuleFor(cp => cp.ConfirmPassword)
                .Equal(p => p.Password)
                .WithMessage("Hasła nie są takie same!");
        }

    }
}
