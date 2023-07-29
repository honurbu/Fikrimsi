using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fikrimsi.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentDto commentv)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Comment comment = new Comment
            {
                CommentDate = DateTime.Now,
                Content = commentv.Content,
                TitleId = commentv.TitleId,

                UserAppId = userId
            };

            // Save the comment using the commentService or perform any other desired action.
            // For example:
            // var result = await _commentService.AddCommentAsync(comment);

            return Ok(comment);
        }

    }
}
