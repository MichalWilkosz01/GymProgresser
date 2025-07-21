using GymProgresser.Application.Progress.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Progress.Interfaces
{
    public interface IProgressService
    {
        public Task<ExerciseHistory> GetExerciseHistory(int userId, int exerciseId);
        public Task<RegressionCoefficients> GetPredictionCoefficients(int userId, int exerciseId, int predictionPoints);
        public double GetOneRepMax(double weight, int reps);
    }
}
