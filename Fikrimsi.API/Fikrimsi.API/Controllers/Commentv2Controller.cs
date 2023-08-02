using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Services;
using Fikrimsi.Repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text.Json;

namespace Fikrimsi.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Commentv2Controller : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly AppDbContext _appDbContext;
        public Commentv2Controller(ICommentService commentService, AppDbContext appDbContext)
        {
            _commentService = commentService;
            _appDbContext = appDbContext;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentDto commentv)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            commentv.UserAppId = userId; // UserAppId'yi doğrudan doldurun

            CommentDto comment = new CommentDto
            {
                CommentDate = DateTime.Now,
                Content = commentv.Content,
                TitleId = commentv.TitleId,
                UserAppId = commentv.UserAppId, // UserAppId'yi aktarın
            };

            await _commentService.AddAsync(commentv);

            return Ok(comment);
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserComment()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comments = _appDbContext.Comments.Where(x => x.UserAppId == userId)
                .Include(x => x.Title);
            return Ok(comments);
           
        }
    }
}
