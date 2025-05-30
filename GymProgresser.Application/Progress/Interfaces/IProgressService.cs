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
        ExerciseHistory GetExerciseHistory(int userId, int exerciseId);
    }
}
