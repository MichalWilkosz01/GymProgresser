using GymProgresser.Application.Exercises.Dtos;
using GymProgresser.Application.Exercises.Interfaces;
using GymProgresser.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymProgresser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        public ExercisesController(IExerciseService service)
        {
            _exerciseService = service;
        }
        [HttpPost]
        public async Task<IActionResult> PostExercise([FromBody] PostExerciseRequestDto postExerciseRequestDto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                var res = await _exerciseService.PostExerciseAsync(postExerciseRequestDto, userId);
                return Ok(res);
            }

            return Unauthorized();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetExerciseById([FromRoute]int id)
        {
            var res = await _exerciseService.GetExerciseByIdAsync(id);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetExercises([FromQuery] string? category = null)
        {
            if (string.IsNullOrEmpty(category))
            {
                var all = await _exerciseService.GetAllExercisesAsync();
                return Ok(all);
            }
            var res = await _exerciseService.GetExercisesByCategoryAsync(category);
            return Ok(res);
        }
    }
}
