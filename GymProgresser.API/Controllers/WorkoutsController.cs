using GymProgresser.Application.Workouts.Dtos;
using GymProgresser.Application.Workouts.Interfaces;
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
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet("{workoutId:int}")]
        public async Task<IActionResult> GetWorkoutById([FromRoute] int workoutId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                var res = await _workoutService.GetWorkoutByIdAsync(workoutId, userId);
                return Ok(res);
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserWorkouts()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                var res = await _workoutService.GetWorkoutListAsync(userId);
                return Ok(res);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> PostWorkout([FromBody] PostWorkoutRequestDto workoutRequestDto)
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
        public async Task<IActionResult> UpdateWorkout([FromBody] UpdateWorkoutRequestDto workoutRequestDto)
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
