using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Interfaces
{
    public interface IWorkoutRepository
    {
        public Task PostWorkoutAsync(Workout workout, int userId);
        public Task<Workout?> GetWorkoutByIdAsync(int workoutId);
        public Task<List<Workout>> GetWorkoutsByUserIdAsync(int userId);
        public Task UpdateWorkoutAsync(Workout workout);
        public Task DeleteWorkoutAsync(Workout workout);
    }
}
