using Blog.Core;
using Blog.Core.Services;
using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Infrastructure.Oauth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Authenticate.OAuth
{
    public class GitHubOAuthController : AuthenticateController
    {
        private readonly GitHubOauth gitHubOauth;

        public GitHubOAuthController(BlogContext blogContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, GitHubOauth gitHubOauth) : base(blogContext, userManager, signInManager)
        {
            this.gitHubOauth = gitHubOauth;
        }

        [Route("oauth/github/authorize")]
        public IActionResult Authorize()
        {
            var loginUrl = gitHubOauth.GetAuthorizeUrl();

            return Redirect(loginUrl);
        }

        [Route("oauth/github/login")]
        public async Task<IActionResult> Login(string code)
        {
            var user = await gitHubOauth.GetCurrentUser(code);

            await SignIn(user);

            return Redirect("/");
        }
    }
}
