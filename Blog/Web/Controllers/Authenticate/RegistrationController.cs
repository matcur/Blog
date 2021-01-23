using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Core.FilterAttributes;
using Blog.Web.Controllers.Authenticate;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Blog.Core.FilterAttributes.Authorization;
using Blog.Core.FilterAttributes.Actions;

namespace WebApplication.Web.Controllers.Authenticate
{
    public class RegistrationController : AuthenticateController
    {
        public RegistrationController(BlogContext blogContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : base(blogContext, userManager, signInManager) { }

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
            var user = ApplicationUser.MakeFrom(register);
            
            await Register(user);

            return Redirect("/");
        }
    }
}
