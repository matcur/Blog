using Blog.DataAccess;
using Blog.DataAccess.Models;
using Blog.Extensions;
using Blog.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PostService
    {
        private readonly BlogContext blogContext;
        
        private readonly DbSet<Post> postTable;

        public PostService(BlogContext blogContext)
        {
            this.blogContext = blogContext;
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
                            .ThenInclude(c => c.Autor)
                            .First(p => p.Id == id);
        }

        public void Create(Post post)
        {
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

        public void AssociateComment(Comment comment, long postId)
        {
            var post = FindPost(postId);

            post.Comments.Add(comment);
            blogContext.SaveChanges();
        }
    }
}
