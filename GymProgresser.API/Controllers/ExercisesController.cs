using GymProgresser.Application.Exercises.Dtos;
using GymProgresser.Application.Exercises.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymProgresser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _service;
        public ExercisesController(IExerciseService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> PostExercise([FromBody] PostExerciseRequestDto postExerciseRequestDto)
        {
            return Ok();
        }
    }
}
