using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public class UserService
    {
        public UserManager<User> UserManager { get; }

        public HttpContext HttpContext { get; }

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            UserManager = userManager;
            HttpContext = httpContextAccessor.HttpContext;
        }

        public async Task<User> GetCurrentUser()
        {
            return await UserManager.GetUserAsync(HttpContext.User);
        }

        public bool IsUserAuth()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
