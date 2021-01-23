using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Web.Controllers.Authenticate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication.Web.Controllers.Authenticate
{
    public class LogoutController : AuthenticateController
    {
        public LogoutController(BlogContext blogContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : base(blogContext, userManager, signInManager) { }

        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await SignOut();

            return Redirect("/login");
        }
    }
}
