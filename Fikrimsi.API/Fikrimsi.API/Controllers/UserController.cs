using AutoMapper;
using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Services;
using Fikrimsi.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fikrimsi.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        private readonly IMapper _mapper;


        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            
            
                return Ok(await _userService.CreateUserAsync(createUserDto));
           
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                // Retrieve the user's unique identifier (id) from the authenticated user's claims
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in the token.");
                }

                // Fetch the user from the database based on the user id
                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
