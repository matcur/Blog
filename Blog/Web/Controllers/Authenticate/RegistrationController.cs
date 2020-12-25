using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Infrastructure.FilterAttributes;
using Blog.Web.Controllers.Authenticate;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApplication.Web.Controllers.Authenticate
{
    public class RegistrationController : AuthenticateController
    {
        public RegistrationController(BlogContext blogContext, UserManager<User> userManager, SignInManager<User> signInManager) : base(blogContext, userManager, signInManager) { }

        [HttpGet]
        [OnlyAnonymous]
        [Route("/register")]
        public IActionResult Register()
        {
            return View("~/Web/Views/Authorization/Register.cshtml");
        }

        [HttpPost]
        [ValidateModel]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            var user = Blog.DataAccess.Models.User.MakeFrom(register);
            var identyResult = await UserManager.CreateAsync(user, user.PasswordHash);

            VerifyIdentyResult(identyResult);
            await SignIn(user);

            return Redirect("/");
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
