using Microsoft.AspNetCore.Mvc;

namespace jwtapi.Controllers
{
    [Route("api/jwt")]
    [ApiController]
    public class JwtController : Controller
    {
        private readonly ILogger<JwtController> _logger;
        private readonly JwtManager _jwtManager;

        public JwtController(ILogger<JwtController> logger, JwtManager jwtManager)
        {
            _logger = logger;
        }
        [HttpGet("generate")]
        public async Task<IActionResult> Generate([FromBody] Dictionary<string, string> data, int expiresInDays)
        {
            return Ok(_jwtManager.Generate(data, expiresInDays));
        }
    }
}