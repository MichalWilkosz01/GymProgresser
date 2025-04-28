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
        public Task<int> PostWorkoutAsync(WorkoutRequestDto workoutRequestDto, int userId);
        public Task UpdateWorkoutAsync(WorkoutRequestDto workoutRequestDto, int userId);
        public Task DeleteWorkoutAsync(int workoutId, int userId);
        public Task<Workout?> GetWorkoutByIdAsync(int workoutId, int userId);
        public Task<List<Workout>> GetWorkoutListAsync(int userId);
    }
}
