using Blog.DataAccess;
using Blog.DataAccess.Extensions;
using Blog.DataAccess.Models;
using Blog.Core.FilterAttributes;
using Blog.Core.Services;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class PostController : BlogController
    {
        public PostController(BlogContext blogContext, UserService userService) : base(blogContext, userService) { }

        [HttpGet]
        [Route("/posts")]
        public ActionResult Index([FromQuery(Name = "page")] int pageNumber = 1)
        {
            var allPostCount = DbPost.Count();
            var posts = DbPost.Paginate(pageNumber, 2).ToList();
            ViewBag.PageNavigation = new PageNavigationViewModel(
                pageNumber, allPostCount / 2, new UriBuilder($"https://{Request.Host}" + Url.Action("Index"))
                );

            return View(posts);
        }

        [HttpGet]
        [Route("/posts/details/{id:long}")]
        public ActionResult Details(long id)
        {
            var post = DbPost.Find(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpGet]
        [Authorize]
        [Route("/posts/create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        [Route("/posts/create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post post)
        {
            post.CreatedAt = DateTime.Now;
            post.Author = await UserService.GetCurrentUser();
            DbPost.Add(post);
            BlogContext.SaveChanges();

            return View();
        }

        [HttpGet]
        [Route("/posts/edit/{id:long}")]
        public ActionResult Edit(long id)
        {
            var post = DbPost.Find(id);
            if (post == null)
                return NotFound($"Post {id} not found");

            return View(post);
        }

        [HttpPost]
        [Route("/posts/edit/{id:long}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post updatingPost)
        {
            var post = DbPost.Find(updatingPost.Id);
            if (post == null)
                return NotFound();

            post.Content = updatingPost.Content;
            post.Title = updatingPost.Title;

            BlogContext.Entry(post).State = EntityState.Modified;
            BlogContext.SaveChanges();
            
            return Redirect("/posts");
        }

        [HttpDelete]
        [Route("/posts/delete/{id:long}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = DbPost.Find(id);
            if (post == null)
                return NotFound($"Post {id} not found");

            BlogContext.Entry(post).State = EntityState.Deleted;
            BlogContext.SaveChanges();

            return View();
        }
    }
}
