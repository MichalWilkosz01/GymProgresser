using GymProgresser.Application.Exercises.Dtos;
using GymProgresser.Application.Exercises.Interfaces;
using GymProgresser.Application.Exercises.Mappers;
using GymProgresser.Application.ExercisesWorkouts.Dtos;
using GymProgresser.Application.Users.Interfaces;
using GymProgresser.Domain.Entities;
using GymProgresser.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Exercises
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        public ExerciseService(IExerciseRepository exerciseRepository, IUserRepository userRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
        }

        public async Task<List<GetExerciseResponseDto>> GetAllExercisesAsync()
        {
            var exercises = await _exerciseRepository.GetAllExercisesAsync();
            List<GetExerciseResponseDto> res = new List<GetExerciseResponseDto>();
            if (exercises != null)
            {
                res = exercises
                        .Select(ExerciseMapper.GetExerciseDtoFromEntity)
                        .ToList();
            }
            
            return res;
        }

        public async Task<GetExerciseResponseDto> GetExerciseByIdAsync(int id)
        {
            var exercise = await _exerciseRepository.GetExerciseAsync(id);
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with id {id} was not found.");

            var res = ExerciseMapper.GetExerciseDtoFromEntity(exercise);

            return res;
        }

        public async Task<List<GetExerciseResponseDto>> GetExercisesByCategoryAsync(string category)
        {
            if (!Enum.TryParse<ExerciseCategory>(category, ignoreCase: true, out var parsedCategory))
                throw new ArgumentException($"Invalid category: {category}");

            var exercises = await _exerciseRepository.GetAllExercisesByCategoryAsync(parsedCategory);
            List<GetExerciseResponseDto> res = new List<GetExerciseResponseDto> ();
            if (exercises != null)
            {
                res = exercises.Select(ExerciseMapper.GetExerciseDtoFromEntity).ToList();
            }

            return res;
        }

        public async Task<List<GetExerciseResponseDto>> GetVerifiedExercisesAsync()
        {
            var exercises = await _exerciseRepository.GetAllVerifiedExercisesAsync();
            List<GetExerciseResponseDto> res = new List<GetExerciseResponseDto>();
            if (exercises != null)
            {
                res = exercises.Select(ExerciseMapper.GetExerciseDtoFromEntity).ToList();
            }

            return res;
        }

        public async Task<List<GetExerciseResponseDto>> GetVerifiedExercisesByCategoryAsync(string category)
        {
            if (!Enum.TryParse<ExerciseCategory>(category, ignoreCase: true, out var parsedCategory))
                throw new ArgumentException($"Invalid category: {category}");

            var exercises = await _exerciseRepository.GetAllVerifiedExercisesByCategoryAsync(parsedCategory);
            List<GetExerciseResponseDto> res = new List<GetExerciseResponseDto>();
            if (exercises != null)
            {
                res = exercises.Select(ExerciseMapper.GetExerciseDtoFromEntity).ToList();
            }

            return res;
        }

        public async Task<int> PostExerciseAsync(PostExerciseRequestDto exerciseRequestDto, int userId)
        {
            var user = await GetAndValidateUserAsync(userId);
            var exercise = ExerciseMapper.GetExerciseFromDto(exerciseRequestDto, user.Id);
            await _exerciseRepository.PostExerciseAsync(exercise, userId);
            return exercise.Id;
        }
        private async Task<User> GetAndValidateUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {userId} was not found.");
            }

            return user;
        }
    }
}
