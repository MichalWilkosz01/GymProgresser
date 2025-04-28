using GymProgresser.Application.Workouts.Dtos;
using GymProgresser.Application.Workouts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymProgresser.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpPost]
        public async Task<IActionResult> PostWorkout([FromBody] WorkoutRequestDto workoutRequestDto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                var res = await _workoutService.PostWorkoutAsync(workoutRequestDto, userId);
                return Ok(res);
            }

            return Unauthorized();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkout([FromBody] WorkoutRequestDto workoutRequestDto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                await _workoutService.UpdateWorkoutAsync(workoutRequestDto, userId);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("{workoutId}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] int workoutId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                await _workoutService.DeleteWorkoutAsync(workoutId, userId);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
