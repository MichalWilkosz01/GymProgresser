using FluentValidation;
using GymProgresser.Application.Auth.Dtos;
using GymProgresser.Application.Users.Interfaces;
using GymProgresser.Application.Workouts.Dtos;
using GymProgresser.Application.Workouts.Interfaces;
using GymProgresser.Application.Workouts.Mapper;
using GymProgresser.Domain.Entities;

namespace GymProgresser.Application.Workouts
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IValidator<PostWorkoutRequestDto> _validatorPostWorkoutRequestDto;
        private readonly IValidator<UpdateWorkoutRequestDto> _validatoruUpdateWorkoutRequestDto;
        public WorkoutService(IUserRepository userRepository, IWorkoutRepository workoutRepository,
                                IValidator<PostWorkoutRequestDto> validatorPostWorkoutRequestDto, 
                                IValidator<UpdateWorkoutRequestDto> validatoruUpdateWorkoutRequestDto)
        {
            _userRepository = userRepository;
            _workoutRepository = workoutRepository;
            _validatorPostWorkoutRequestDto = validatorPostWorkoutRequestDto;
            _validatoruUpdateWorkoutRequestDto = validatoruUpdateWorkoutRequestDto;
        }

        public async Task<int> PostWorkoutAsync(PostWorkoutRequestDto workoutRequestDto, int userId)
        {
            var validationResult = await _validatorPostWorkoutRequestDto.ValidateAsync(workoutRequestDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await GetAndValidateUserAsync(userId);

            var res = WorkoutMapper.WorkoutRequestDtoToWorkout(workoutRequestDto);
            if (workoutRequestDto.Exercises != null && workoutRequestDto.Exercises.Any())
            {
                res.ExercisesWorkout = workoutRequestDto.Exercises
                    .Select(exerciseDto => new ExerciseWorkout
                    {
                        ExerciseId = exerciseDto.ExerciseId, 
                        Sets = exerciseDto.Sets,
                        Reps = exerciseDto.Reps,
                        WeightKg = exerciseDto.WeightKg
                    })
                    .ToList();
            }
            await _workoutRepository.PostWorkoutAsync(res, userId);

            return res.Id;
        }

        public async Task UpdateWorkoutAsync(UpdateWorkoutRequestDto updateWorkoutRequestDto, int userId)
        {
            if (updateWorkoutRequestDto.Id == null)
                throw new ArgumentException("Workout ID must be provided for update.");

            var validationResult = await _validatoruUpdateWorkoutRequestDto.ValidateAsync(updateWorkoutRequestDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var workoutToUpdate = await GetAndValidateWorkoutAsync(updateWorkoutRequestDto.Id.Value, userId);

            WorkoutMapper.UpdateWorkoutFromDto(workoutToUpdate, updateWorkoutRequestDto);

            await _workoutRepository.UpdateWorkoutAsync(workoutToUpdate);
        }

        public async Task DeleteWorkoutAsync(int workoutId, int userId)
        {
            var workout = await GetAndValidateWorkoutAsync(workoutId, userId);

            await _workoutRepository.DeleteWorkoutAsync(workout);
        }

        public async Task<GetWorkoutResponseDto> GetWorkoutByIdAsync(int workoutId, int userId)
        {
            var workout = await GetAndValidateWorkoutAsync(workoutId, userId);

            if (workout == null)
                throw new KeyNotFoundException($"Workout with ID {workoutId} not found.");

            return WorkoutMapper.GetWorkoutDtoFromWorkout(workout);
        }

        public async Task<List<GetWorkoutResponseDto>> GetWorkoutListAsync(int userId)
        {
            var user = await GetAndValidateUserAsync(userId);
            var workouts = await _workoutRepository.GetWorkoutsByUserIdAsync(userId);
            return workouts.Select(w => WorkoutMapper.GetWorkoutDtoFromWorkout(w)).ToList();
        }

        private async Task<Workout> GetAndValidateWorkoutAsync(int workoutId, int userId)
        {
            var workout = await _workoutRepository.GetWorkoutByIdAsync(workoutId);

            if (workout == null)
                throw new KeyNotFoundException($"Workout with ID {workoutId} not found.");

            if (workout.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to access this workout.");

            return workout;
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
