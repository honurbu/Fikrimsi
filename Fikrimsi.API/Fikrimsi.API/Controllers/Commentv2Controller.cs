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
        AppDbContext _appDbContext;
        public Commentv2Controller(ICommentService commentService, AppDbContext appDbContext)
        {
            _commentService = commentService;
            _appDbContext = appDbContext;
        }


        [HttpPost]
        [Authorize]
        //[Authorize(Roles="Yorumcu")]
        //token not contain roles. So return Forbidden error. 
        public async Task<IActionResult> AddComment(CommentDto commentv)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //string userIdJson = JsonSerializer.Deserialize(userId);
           
            Comment comment = new Comment
            {
                CommentDate = DateTime.Now,
                Content = commentv.Content, 
                UserAppId = userId.ToString(),
                TitleId = commentv.TitleId,
            };


            Console.WriteLine(comment.UserAppId);
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


        #region
        /*
         
            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);


            var users = dbContext.Users.Where(p => p.Id == userIdClaim.Value)
                .Include(p => p.Allergies)
                .Select(p => p.Allergies);

            return Ok(users.FirstOrDefault());
        */
        #endregion



    }
}
