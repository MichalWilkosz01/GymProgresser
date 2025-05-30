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
        public IActionResult GetPredictProgress([FromRoute] int exerciseId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok();
        }
    }
}
