using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Infrastructure.Services;
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
        public UserService UserService => userService;

        public BlogContext BlogContext => blogContext;

        public DbSet<Post> DbPost => dbPost;

        private readonly BlogContext blogContext;

        private readonly UserService userService;

        private readonly DbSet<Post> dbPost;

        public BlogController(BlogContext blogContext, UserService userService)
        {
            this.blogContext = blogContext;
            this.userService = userService;
            dbPost = blogContext.Posts;
        }
    }
}
