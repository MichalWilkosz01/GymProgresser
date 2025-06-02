using GymProgresser.Application.Exercises.Interfaces;
using GymProgresser.Application.ExercisesWorkouts.Interfaces;
using GymProgresser.Application.Progress.Dtos;
using GymProgresser.Application.Progress.Interfaces;
using GymProgresser.Application.Users.Interfaces;
using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Progress
{
    public class ProgressService : IProgressService
    {
        private readonly IUserRepository _userRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IExerciseWorkoutRepository _exerciseWorkoutRepository;
        public ProgressService(IUserRepository userRepository, 
                                IExerciseRepository exerciseRepository,
                                IExerciseWorkoutRepository exerciseWorkoutRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
            _exerciseWorkoutRepository = exerciseWorkoutRepository;
        }
        public async Task<ExerciseHistory> GetExerciseHistory(int userId, int exerciseId)
        {
            var allPerformedExercises = await GetUserExerciseHistoryAsync(userId, exerciseId);

            var history = allPerformedExercises.Select((pe, index) => new DataPoint
            {
                X = index + 1, 
                Y = pe.WeightKg * pe.Reps,
                Reps = pe.Reps,
                Sets = pe.Sets,
                WeightKg = pe.WeightKg
                
            }).ToList();

            var exercise = await _exerciseRepository.GetExerciseAsync(exerciseId);

            ExerciseHistory res = new ExerciseHistory()
            {
                ExerciseName = exercise.Name,
                History = history
            };

            return res;
        }

        public async Task<List<DataPoint>> GetPrediction(int userId, int exerciseId, int predictionPoints)
        {
            var allPerformedExercises = await GetUserExerciseHistoryAsync(userId, exerciseId);
            // 1. Przygotuj dane historyczne do regresji
            var x = Enumerable.Range(1, allPerformedExercises.Count).Select(i => (double)i).ToArray();
            var y = allPerformedExercises
                .Select(e => e.Reps * e.WeightKg * e.Sets) // objętość treningowa
                .Select(v => (double)v)
                .ToArray();

            // 2. Oblicz regresję liniową
            var (slope, intercept) = CalculateLinearRegression(x, y);

            // 3. Generuj przyszłe punkty
            var prediction = new List<DataPoint>();
            int nextIndex = allPerformedExercises.Count + 1;

            for (int i = 0; i < predictionPoints; i++)
            {
                var xVal = nextIndex + i;
                var predictedVolume = slope * xVal + intercept;

                prediction.Add(new DataPoint
                {
                    X = xVal,             // np. nr sesji
                    Y = predictedVolume
                });
            }

            return prediction;
        }

        private async Task<List<ExerciseWorkout>> GetUserExerciseHistoryAsync(int userId, int exerciseId)
        {
            return (await _exerciseWorkoutRepository.GetUserExerciseWorkoutByExerciseAsync(userId, exerciseId))
                                           .OrderBy(ew => ew.Workout.Date)
                                           .ToList();
        }

        private (double slope, double intercept) CalculateLinearRegression(double[] x, double[] y)
        {
            int n = x.Length;
            double sumX = x.Sum();
            double sumY = y.Sum();
            double sumXY = x.Zip(y, (xi, yi) => xi * yi).Sum();
            double sumX2 = x.Select(xi => xi * xi).Sum();

            double slope = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
            double intercept = (sumY - slope * sumX) / n;

            return (slope, intercept);
        }
    }
}
