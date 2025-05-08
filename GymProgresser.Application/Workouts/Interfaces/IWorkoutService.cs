using GymProgresser.Application.Workouts.Dtos;
using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Interfaces
{
    public interface IWorkoutService
    {
        public Task<int> PostWorkoutAsync(PostWorkoutRequestDto workoutRequestDto, int userId);
        public Task UpdateWorkoutAsync(UpdateWorkoutRequestDto workoutRequestDto, int userId);
        public Task DeleteWorkoutAsync(int workoutId, int userId);
        public Task<GetWorkoutResponseDto> GetWorkoutByIdAsync(int workoutId, int userId);
        public Task<List<GetWorkoutResponseDto>> GetWorkoutListAsync(int userId);
    }
}
