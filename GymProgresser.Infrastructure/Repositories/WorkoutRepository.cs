using GymProgresser.Application.Workouts.Interfaces;
using GymProgresser.Domain.Entities;
using GymProgresser.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly GymProgressDbContext _dbContext;
        public WorkoutRepository(GymProgressDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Workout?> GetWorkoutByIdAsync(int workoutId)
        {
            var res = await _dbContext.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId);
            return res;
        }

        public async Task<List<Workout>?> GetWorkoutsByUserIdAsync(int userId)
        {
            var res = await _dbContext.Workouts.Where(w => w.UserId == userId).ToListAsync();
            return res;
        }

        public async Task PostWorkoutAsync(Workout workout, int userId)
        {
            workout.UserId = userId;
            await _dbContext.Workouts.AddAsync(workout);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateWorkoutAsync(Workout workout)
        {
            _dbContext.Workouts.Update(workout);
            await _dbContext.SaveChangesAsync();
        }
    }
}
