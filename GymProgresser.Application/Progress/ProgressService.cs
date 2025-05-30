using GymProgresser.Application.Exercises.Interfaces;
using GymProgresser.Application.Progress.Dtos;
using GymProgresser.Application.Progress.Interfaces;
using GymProgresser.Application.Users.Interfaces;
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
        public ProgressService(IUserRepository userRepository, IExerciseRepository exerciseRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }
        public ExerciseHistory GetExerciseHistory(int userId, int exerciseId)
        {
            //_userRepository.ge
            throw new NotImplementedException();
        }
    }
}
