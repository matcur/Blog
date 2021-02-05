using Blog.Core.Services;
using Blog.DataAccess;
using Blog.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Blog.Tests.Core.Services
{
    public class PostServiceTest
    {
        [Fact]
        public void GetCountEqualZero()
        {
            var posts = new List<Post>();

            var service = new PostService(MakeBlogContext(posts));

            Assert.Equal(0, service.GetCount());
        }

        [Fact]
        public void GetCountEqualOne()
        {
            var posts = new List<Post>
            {
                new Post(),
            };

            var service = new PostService(MakeBlogContext(posts));

            Assert.Equal(1, service.GetCount());
        }

        private BlogContext MakeBlogContext(List<Post> posts)
        {
            var blogContext = new Mock<BlogContext>();
            var postSet = new Mock<DbSet<Post>>();

            postSet.As<IQueryable<Post>>().Setup(q => q.GetEnumerator()).Returns(posts.GetEnumerator());
            blogContext.Setup(context => context.Posts).Returns(postSet.Object);

            return blogContext.Object;
        }
    }
}
