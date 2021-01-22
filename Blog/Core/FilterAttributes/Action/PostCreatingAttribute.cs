using Blog.Core.Services;
using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Core.FilterAttributes.Action
{
    public class PostCreatingAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var post = (Post)context.ActionArguments["post"];
            var services = context.HttpContext.RequestServices;

            post.CreatedAt = DateTime.Now;
            post.Author = await services.GetService<UserService>().GetCurrentUser();
        }
    }
}
