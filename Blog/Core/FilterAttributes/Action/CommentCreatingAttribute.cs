using Blog.Core.Services;
using Blog.DataAccess;
using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.FilterAttributes.Action
{
    public class CommentCreatingAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var comment = (Comment)context.ActionArguments["comment"];
            var postId = (long)context.ActionArguments["postId"];
            var services = context.HttpContext.RequestServices;

            comment.CreatedAt = DateTime.Now;
            comment.Autor = services.GetService<UserService>().GetCurrentUser().GetAwaiter().GetResult();
            comment.Post = services.GetService<BlogContext>().Posts.First(p => p.Id == postId);
        }
    }
}
