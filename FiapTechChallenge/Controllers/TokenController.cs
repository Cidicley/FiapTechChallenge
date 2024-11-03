using Core.Models;
using FiapTechChallengeApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapTechChallengeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // Injeção da nossa interface para geração de Token
        private readonly ITokenService _tokenService;
        
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            var token = _tokenService.GetToken(usuario);

            if (!string.IsNullOrWhiteSpace(token))
            {
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}
