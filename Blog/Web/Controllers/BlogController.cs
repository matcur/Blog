using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public abstract class BlogController : Controller
    {
        protected readonly UserService userService;

        protected readonly BlogContext blogContext;

        protected readonly DbSet<Post> dbPost;

        public BlogController(BlogContext blogContext, UserService userService)
        {
            this.blogContext = blogContext;
            this.userService = userService;
            dbPost = blogContext.Posts;
        }
    }
}
