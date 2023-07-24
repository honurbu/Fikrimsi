using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fikrimsi.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);

            if (result == null)
            {
                return NotFound("Email or Password Wrong !");
            }
            else
                return Ok(result);
        }
    }
}
