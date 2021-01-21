using Blog.Core.FilterAttributes;
using Blog.Core.Services;
using Blog.DataAccess;
using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class CommentPostController : BlogController
    {
        public CommentPostController(BlogContext blogContext, UserService userService) : base(blogContext, userService) { }

        [Authorize]
        [ValidateModel]
        [Route("posts/{id}/comment/create")]
        public async Task<IActionResult> Create(Comment comment, long id)
        {
            var post = postTable.First(p => p.Id == id);
            if (post == null)
                return NotFound();

            comment.Autor = await userService.GetCurrentUser();
            comment.CreatedAt = DateTime.Now;
            comment.Post = post;

            commentTable.Add(comment);
            await blogContext.SaveChangesAsync();

            return RedirectToAction("Details", "Post", new { id = id });
        }
    }
}
