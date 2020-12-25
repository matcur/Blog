using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Infrastructure.FilterAttributes;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Authenticate
{
    public class LoginController : AuthenticateController
    {
        public LoginController(BlogContext blogContext, UserManager<User> userManager, SignInManager<User> signInManager) : base(blogContext, userManager, signInManager) { }

        [HttpGet]
        [OnlyAnonymous]
        [Route("/login")]
        public IActionResult Login()
        {
            return View("~/Web/Views/Authorization/Login.cshtml");
        }

        [HttpPost]
        [OnlyAnonymous]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = await GetValidatedUserAsync(login);
            await SignIn(user);

            return Redirect("~/");
        }

        private async Task<User> GetValidatedUserAsync(LoginViewModel login)
        {
            var user = await UserManager.FindByNameAsync(login.Name);
            if (user == null)
                throw new Exception($"wrong user name {login.Name}");

            var isValidPassword = await UserManager.CheckPasswordAsync(user, login.Password);
            if (!isValidPassword)
                throw new Exception($"wrong password");

            return user;
        }
    }
}
