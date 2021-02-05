using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Extensions;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PostService
    {
        private readonly DbSet<Post> postTable;

        private readonly BlogContext blogContext;
        
        private readonly UserService userService;

        private readonly BlogFile blogFile;
        
        private readonly UserManager<ApplicationUser> userManager;

        public PostService(BlogContext blogContext, UserManager<ApplicationUser> userManager, UserService userService, BlogFile blogFile)
        {
            this.blogContext = blogContext;
            this.userManager = userManager;
            this.userService = userService;
            this.blogFile = blogFile;
            postTable = blogContext.Posts;
        }

        public int GetCount()
        {
            return postTable.Count();
        }

        public List<Post> GetPaginate(int page, int perPage)
        {
            return postTable.Paginate(page, perPage).ToList();
        }

        public Post FindPost(long id)
        {
            return postTable.First(p => p.Id == id);
        }

        public Post FindPostDetail(long id)
        {
            return postTable.Include(p => p.Comments)
                            .ThenInclude(c => c.Author)
                            .First(p => p.Id == id);
        }

        public async Task Create(Post post, IFormFile thumbnail)
        {
            var currentUser = await userService.GetCurrentUser();
            post.AuthorId = currentUser.Id;
            post.ThumbnailPath = blogFile.Save(thumbnail);

            postTable.Add(post);
            blogContext.SaveChanges();
        }

        public void Update(Post updatingPost)
        {
            var updatedPost = postTable.First(p => p.Id == updatingPost.Id);

            updatedPost.Content = updatingPost.Content;
            updatedPost.Title = updatingPost.Title;

            blogContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var post = postTable.First(p => p.Id == id);

            blogContext.Entry(post).State = EntityState.Deleted;
            blogContext.SaveChanges();
        }

        public async Task AssociateComment(Comment comment, long postId)
        {
            var post = FindPost(postId);
            var currentUser = await userService.GetCurrentUser();
            comment.AuthorId = currentUser.Id;

            post.Comments.Add(comment);
            blogContext.SaveChanges();
        }
    }
}
