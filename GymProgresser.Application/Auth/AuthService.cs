using FluentValidation;
using GymProgresser.Application.Auth.Classes;
using GymProgresser.Application.Auth.Dtos;
using GymProgresser.Application.Auth.Interfaces;
using GymProgresser.Application.Profiles;
using GymProgresser.Application.Users.Interfaces;
using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IValidator<RegisterRequestDto> _validatorRegister;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IJwtService _jwtService;

        public AuthService(IValidator<RegisterRequestDto> validatorRegister, IUserRepository userRepository, 
                        IPasswordManager passwordManager, IJwtService jwtService)
        {
            _validatorRegister = validatorRegister;
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _jwtService = jwtService;
        }
        public async Task<string> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginRequestDto.Email);

            if (user is null)
            {
                throw new UnauthorizedAccessException("Nieprawidłowy adres email lub hasło.");
            }


            var isPasswordValid = _passwordManager.VerifyPassword(new PasswordVerificationData(user, loginRequestDto.Password));

            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Nieprawidłowy adres email lub hasło.");

            var res = _jwtService.GenerateToken(user.Id, user.Email);

            return res;
        }


        public async Task<string> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var validationResult = await _validatorRegister.ValidateAsync(registerRequestDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var userFromDb = await _userRepository.GetUserByEmailAsync(registerRequestDto.Email);

            if (userFromDb is not null)
            {
                throw new InvalidOperationException("Użytkownik o tym adresie email już istnieje.");
            }

            var passwordHashAndSalt = _passwordManager.HashPassword(registerRequestDto.Password);

            var user = new User()
            {
                CreatedAt = DateTime.Now,
                PasswordHash = passwordHashAndSalt.Hash,
                Salt = passwordHashAndSalt.Salt,
                Email = registerRequestDto.Email
            };

            user.Id = await _userRepository.AddUserAsync(user);

            var res = _jwtService.GenerateToken(user.Id, user.Email);

            return res;
            //throw new NotImplementedException();
        }
    }
}
