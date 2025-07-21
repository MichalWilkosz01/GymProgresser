using GymProgresser.Application.ExercisesWorkouts.Interfaces;
using GymProgresser.Application.Progress.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymProgresser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;

        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpGet("{exerciseId}/predict")]
        public async Task<IActionResult> GetPredictProgress([FromRoute] int exerciseId, [FromQuery] int predictionPoints)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                var res = await _progressService.GetPredictionCoefficients(userId, exerciseId, predictionPoints);
                return Ok(res);
            }
            return Unauthorized();
        }

        [HttpGet("history/{exerciseId}")]
        public async Task<IActionResult> GetExerciseHistory([FromRoute] int exerciseId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                var res = await _progressService.GetExerciseHistory(userId, exerciseId);
                return Ok(res);
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("1-rep-max")]
        public IActionResult GetOneRepMax(double weight, int reps)
        {
            var res = _progressService.GetOneRepMax(weight, reps);
            return Ok(res);
        }
    }
}
