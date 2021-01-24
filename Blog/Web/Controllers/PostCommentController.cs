using Blog.Core.FilterAttributes;
using Blog.Core.FilterAttributes.Action;
using Blog.Core.FilterAttributes.Actions;
using Blog.Core.Services;
using Blog.DataAccess;
using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class PostCommentController : Controller
    {
        private readonly PostService postService;

        public PostCommentController(PostService postService)
        {
            this.postService = postService;
        }

        [Authorize]
        [ValidateModel]
        [Route("posts/{postId:long}/comment/create")]
        public IActionResult Create(Comment comment, long postId)
        {
            postService.AssociateComment(comment, postId);

            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }
}
