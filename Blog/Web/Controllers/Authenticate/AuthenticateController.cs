using Blog.DataAccess;
using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Authenticate
{
    public abstract class AuthenticateController : Controller
    {
        public BlogContext BlogContext => blogContext;

        public UserManager<ApplicationUser> UserManager => userManager;

        public DbSet<ApplicationUser> UserTable => userTable;

        private readonly BlogContext blogContext;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly DbSet<ApplicationUser> userTable;

        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthenticateController(BlogContext blogContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            userTable = blogContext.Users;
            this.blogContext = blogContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        protected async Task Register(ApplicationUser user)
        {
            var identyResult = await UserManager.CreateAsync(user, user.PasswordHash);

            VerifyIdentyResult(identyResult);
            await SignIn(user);
        }

        protected async Task SignIn(ApplicationUser user)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
        }

        protected async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }

        private void VerifyIdentyResult(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                var s = "";
                foreach (var err in identityResult.Errors)
                    s += err.Description;

                throw new Exception(s);
            }
        }
    }
}
