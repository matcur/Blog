using Blog.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DataAccess
{
    public class BlogContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public BlogContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
