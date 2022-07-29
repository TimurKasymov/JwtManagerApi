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
                     _jwtManager = jwtManager;
              }
              [HttpGet("generate")]
              public async Task<IActionResult> Generate([FromBody] Dictionary<string, string> data, int expiresInDays)
              {
                     _logger.LogInformation("http generate get");
                     return Ok(_jwtManager.Generate(data, expiresInDays));
              }

              [HttpGet("validate")]
              public async Task<IActionResult> Validate([FromBody] string token)
              {
                     _logger.LogInformation("http validate get");
                     return Ok(new { Success = _jwtManager.ValidateToken(token) });
              }

              [HttpGet("extractData")]
              public async Task<IActionResult> ExtractData([FromBody] string token)
              {
                     _logger.LogInformation("http extract data get");
                     return Ok(_jwtManager.GetTokenClaims(token));
              }
       }
}