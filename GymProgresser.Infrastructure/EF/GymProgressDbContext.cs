using GymProgresser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Infrastructure.EF
{
    public class GymProgressDbContext : DbContext
    {
        public GymProgressDbContext(DbContextOptions<GymProgressDbContext> options) : base(options) { }


        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseWorkout> ExercisesWorkouts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        
    }
}
