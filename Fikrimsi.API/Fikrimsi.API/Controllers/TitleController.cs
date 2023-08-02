using Fikrimsi.Core.Services;
using Fikrimsi.Repository.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fikrimsi.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly ITitleService _titleService;
        private readonly AppDbContext _appDbContext;

        public TitleController(ITitleService titleService, AppDbContext appDbContext)
        {
            _titleService = titleService;
            _appDbContext = appDbContext;
        }



        [HttpGet]
        public async Task<IActionResult> Titles()
        {
            var titles = await _titleService.GetAllWithSubjectsAsync();
            return Ok(titles);
        }
    }
}
