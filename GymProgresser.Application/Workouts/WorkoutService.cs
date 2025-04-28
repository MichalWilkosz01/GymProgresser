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
        private readonly IValidator<WorkoutRequestDto> _validatorWorkoutRequestDto;
        public WorkoutService(IUserRepository userRepository, IWorkoutRepository workoutRepository,
                                IValidator<WorkoutRequestDto> validatorWorkoutRequestDto)
        {
            _userRepository = userRepository;
            _workoutRepository = workoutRepository;
            _validatorWorkoutRequestDto = validatorWorkoutRequestDto;
        }

        public async Task<int> PostWorkoutAsync(WorkoutRequestDto workoutRequestDto, int userId)
        {
            var validationResult = await _validatorWorkoutRequestDto.ValidateAsync(workoutRequestDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {userId} was not found.");
            }

            var res = WorkoutMapper.WorkoutRequestDtoToWorkout(workoutRequestDto);
            await _workoutRepository.PostWorkoutAsync(res, userId);

            return res.Id;
        }

        public async Task UpdateWorkoutAsync(UpdateWorkoutRequestDto updateWorkoutRequestDto, int userId)
        {
            if (updateWorkoutRequestDto.Id == null)
                throw new ArgumentException("Workout ID must be provided for update.");

            var validationResult = await _validatorWorkoutRequestDto.ValidateAsync(updateWorkoutRequestDto);

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
    }
}
