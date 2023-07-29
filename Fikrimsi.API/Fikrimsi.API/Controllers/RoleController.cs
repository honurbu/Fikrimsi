using Fikrimsi.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fikrimsi.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<UserApp> _roleManager;

        public RoleController(UserManager<UserApp> userManager, RoleManager<UserApp> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


    }
}
