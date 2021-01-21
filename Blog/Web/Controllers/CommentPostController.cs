using Blog.Core.FilterAttributes;
using Blog.Core.FilterAttributes.Action;
using Blog.Core.FilterAttributes.Actions;
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
        [CommentCreating]
        [Route("posts/{postId:long}/comment/create")]
        public async Task<IActionResult> Create(Comment comment, long postId)
        {
            commentTable.Add(comment);
            await blogContext.SaveChangesAsync();

            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }
}
