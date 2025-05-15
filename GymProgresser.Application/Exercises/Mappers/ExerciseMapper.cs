using GymProgresser.Application.Exercises.Dtos;
using GymProgresser.Domain.Entities;
using GymProgresser.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Exercises.Mappers
{
    public class ExerciseMapper
    {
        public static Exercise GetExerciseFromDto(PostExerciseRequestDto dto, int userId)
        {
            return new Exercise()
            {
                IsVerified = false,
                Name = dto.Name,
                Category = Enum.Parse<ExerciseCategory>(dto.Category, ignoreCase: true),
                CreatedById = userId
            };
        }

        public static GetExerciseResponseDto GetExerciseDtoFromEntity(Exercise exercise)
        {
            return new GetExerciseResponseDto()
            {
                IsVerified = exercise.IsVerified,
                Name = exercise.Name,
                Category = exercise.Category.ToString(),
                Label = EnumHelper.GetEnumDescription(exercise.Category)
            };
        }
    }
}
