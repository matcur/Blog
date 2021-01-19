using Blog.Core;
using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Oauth
{
    public class GitHubOauth
    {
        private readonly IConfiguration configuration;

        private readonly UserManager<ApplicationUser> userManager;

        public GitHubOauth(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public GitHubClient MakeClient()
        {
            var applicationName = configuration["GitHub:ApplicationName"];

            return new GitHubClient(new ProductHeaderValue(applicationName));
        }

        public string GetLoginUrl()
        {
            var client = MakeClient();
            var request = MakeLoginRequest();
            var url = client.Oauth.GetGitHubLoginUrl(request);

            return url.ToString();
        }

        public OauthLoginRequest MakeLoginRequest()
        {
            var clientId = configuration["GitHub:ClientId"];

            return new OauthLoginRequest(clientId);
        }

        public OauthTokenRequest MakeTokenRequest(string code)
        {
            var clientId = configuration["GitHub:ClientId"];
            var clientSecret = configuration["GitHub:ClientSecret"];

            return new OauthTokenRequest(clientId, clientSecret, code);
        }

        public async Task<GitHubClient> MakeAuthorizedClient(string code)
        {
            var client = MakeClient();
            var tokenRequest = MakeTokenRequest(code);
            
            var token = await client.Oauth.CreateAccessToken(tokenRequest);
            client.Credentials = new Credentials(token.AccessToken);

            return client;
        }

        public async Task<ApplicationUser> GetCurrentUser(string code)
        {
            var client = await MakeAuthorizedClient(code);
            var githubUser = await client.User.Current();

            var user = await userManager.FindByNameAsync(githubUser.Login);
            if (user != null)
                return user;

            user = new ApplicationUser
            {
                UserName = githubUser.Login.ToString(),
                PasswordHash = Randomizer.GetString(8),
            };
            await userManager.CreateAsync(user);

            return user;
        }
    }
}
