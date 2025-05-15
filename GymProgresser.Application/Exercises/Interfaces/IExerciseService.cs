using GymProgresser.Application.Exercises.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Exercises.Interfaces
{
    public interface IExerciseService
    {
        public Task<int> PostExerciseAsync(PostExerciseRequestDto exerciseRequestDto, int userId);
        public Task<GetExerciseResponseDto> GetExerciseByIdAsync(int id);
        public Task<List<GetExerciseResponseDto>> GetAllExercisesAsync();
        public Task<List<GetExerciseResponseDto>> GetExercisesByCategoryAsync(string category);
        public Task<List<GetExerciseResponseDto>> GetVerifiedExercisesAsync();
        public Task<List<GetExerciseResponseDto>> GetVerifiedExercisesByCategoryAsync(string category);
    }
}
