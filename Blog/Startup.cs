using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using AspNet.Security.OAuth.GitHub;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blog.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Blog.DataAccess.Models;
using Blog.Core.Services;
using Blog.Infrastructure.Oauth;
using Blog.Extensions;
using Blog.Core;

namespace Blog
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddTransient<PostService>();
            services.AddTransient<GitHubOauth>();
            services.AddTransient<BlogFile>();

            services.AddControllersWithViews();
            services.ConfigureRazorEngineFolders();

            services.AddAuthentication()
                    .AddCookie()
                    .AddOAuth("gitHub", options => 
                    {
                        options.ClientId = Configuration["GitHub:ClientId"];
                        options.ClientSecret = Configuration["GitHub:ClientSecret"];
                        options.AuthorizationEndpoint = Configuration["Github:AuthorizationEndpoint"];
                        options.TokenEndpoint = Configuration["GitHub:TokenEndpoint"];
                        options.UserInformationEndpoint = Configuration["GitHub:UserInformationEndpoint"];
                        options.CallbackPath = Configuration["Github:CallbackPath"];
                    });

            services.AddDbContext<BlogContext>(options =>
                    {
                        var connection = Configuration.GetConnectionString("DefaultConnection");
                        options.UseSqlServer(connection);
                    }, ServiceLifetime.Transient);
            services.AddIdentity<ApplicationUser, IdentityRole<long>>(options =>
                    {
                        var password = options.Password;

                        password.RequireDigit = false;
                        password.RequireLowercase = false;
                        password.RequireNonAlphanumeric = false;
                        password.RequireUppercase = false;
                        password.RequiredLength = 1;

                    })
                    .AddEntityFrameworkStores<BlogContext>()
                    .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
                    {
                        options.LoginPath = "/login/";
                        options.AccessDeniedPath = "/login/";
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
