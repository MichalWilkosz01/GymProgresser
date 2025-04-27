using FluentValidation;
using GymProgresser.Application.Auth.Dtos;
using GymProgresser.Application.Users;
using GymProgresser.Application.Workouts.Dtos;
using GymProgresser.Application.Workouts.Interfaces;
using GymProgresser.Application.Workouts.Mapper;


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

        public async Task UpdateWorkoutAsync(WorkoutRequestDto workoutRequestDto, int userId)
        {
            if (workoutRequestDto.Id == null)
                throw new ArgumentException("Workout ID must be provided for update.");

            var workoutToUpdate = await _workoutRepository.GetWorkoutByIdAsync(workoutRequestDto.Id.Value);

            if (workoutToUpdate == null) 
                throw new KeyNotFoundException($"Workout with ID {workoutRequestDto.Id} not found.");

            if (workoutToUpdate.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to update this workout.");

            WorkoutMapper.UpdateWorkoutFromDto(workoutToUpdate, workoutRequestDto);

            await _workoutRepository.UpdateWorkoutAsync(workoutToUpdate);
            
        }
    }
}
