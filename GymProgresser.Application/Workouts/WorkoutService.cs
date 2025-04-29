using FluentValidation;
using GymProgresser.Application.Auth.Dtos;
using GymProgresser.Application.Users;
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
        private readonly IValidator<PostWorkoutRequestDto> _validatorPostWorkout;
        private readonly IValidator<UpdateWorkoutRequestDto> _validatorUpdateWorkout;
        public WorkoutService(IUserRepository userRepository, IWorkoutRepository workoutRepository,
                                IValidator<PostWorkoutRequestDto> validatorPostWorkout,
                                IValidator<UpdateWorkoutRequestDto> validatorUpdateWorkout)
        {
            _userRepository = userRepository;
            _workoutRepository = workoutRepository;
            _validatorPostWorkout = validatorPostWorkout;
            _validatorUpdateWorkout = validatorUpdateWorkout;
        }

        public async Task<int> PostWorkoutAsync(PostWorkoutRequestDto workoutRequestDto, int userId)
        {
            await ValidateAsync(_validatorPostWorkout, workoutRequestDto);

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {userId} was not found.");
            }

            

            var res = WorkoutMapper.WorkoutRequestDtoToWorkout(workoutRequestDto);
            if (workoutRequestDto.Exercises != null && workoutRequestDto.Exercises.Any())
            {
                await _workoutRepository.PostWorkoutWithExercisesAsync(res, userId, res.ExerciseWorkouts);
            }
            else
            {
                await _workoutRepository.PostWorkoutAsync(res, userId);
            }
            

            return res.Id;
        }

        public async Task UpdateWorkoutAsync(UpdateWorkoutRequestDto updateWorkoutRequestDto, int userId)
        {
            if (updateWorkoutRequestDto.Id == null)
                throw new ArgumentException("Workout ID must be provided for update.");

            await ValidateAsync(_validatorUpdateWorkout, updateWorkoutRequestDto);

            var workoutToUpdate = await GetAndValidateWorkoutAsync(updateWorkoutRequestDto.Id.Value, userId);

            WorkoutMapper.UpdateWorkoutFromDto(workoutToUpdate, updateWorkoutRequestDto);

            await _workoutRepository.UpdateWorkoutAsync(workoutToUpdate);
        }

        public async Task DeleteWorkoutAsync(int workoutId, int userId)
        {
            var workout = await GetAndValidateWorkoutAsync(workoutId, userId);

            await _workoutRepository.DeleteWorkoutAsync(workout);
        }

        public Task<Workout?> GetWorkoutByIdAsync(int workoutId, int userId)
        {

            throw new NotImplementedException();
        }

        public Task<List<Workout>> GetWorkoutListAsync(int userId)
        {
            throw new NotImplementedException();
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

        private async Task ValidateAsync<T>(IValidator<T> validator, T dto)
        {
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
