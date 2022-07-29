using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace jwtapi.Controllers
{
    [Route("api/jwt")]
    [ApiController]
    public class JwtController : Controller
    {
        private readonly ILogger<JwtController> _logger;

        public JwtController(ILogger<JwtController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Generate([FromBody] Dictionary<string, string> data)
        {
            
        }
    }
}