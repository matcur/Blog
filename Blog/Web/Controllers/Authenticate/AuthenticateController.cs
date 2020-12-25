using Blog.DataAccess;
using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Authenticate
{
    public abstract class AuthenticateController : Controller
    {
        public BlogContext BlogContext => blogContext;

        public UserManager<User> UserManager => userManager;

        public DbSet<User> UserTable => userTable;

        private readonly BlogContext blogContext;

        private readonly UserManager<User> userManager;

        private readonly DbSet<User> userTable;

        private readonly SignInManager<User> signInManager;

        public AuthenticateController(BlogContext blogContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            userTable = blogContext.Users;
            this.blogContext = blogContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        protected async Task SignIn(User user)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
        }

        protected async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }
    }
}
