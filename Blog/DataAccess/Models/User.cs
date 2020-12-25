using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DataAccess.Models
{
    public class User : IdentityUser<long>
    {
        public DateTime RegisteredAt { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public static User MakeFrom(RegisterViewModel viewModel)
        {
            return new User
            {
                UserName = viewModel.Name,
                PasswordHash = viewModel.Password,
            };
        }
    }
}
