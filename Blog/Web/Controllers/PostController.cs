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
        public ActionResult Index(int page = 1)
        {
            var allPostCount = dbPost.Count();
            var posts = dbPost.Paginate(page, 2).ToList();

            ViewBag.PageNavigation = new PageNavigationViewModel(
                page, allPostCount / 2, new UriBuilder($"https://{Request.Host}" + Url.Action("Index"))
                );

            return View(posts);
        }

        [HttpGet]
        [Route("/posts/details/{id:long}")]
        public ActionResult Details(long id)
        {
            var post = dbPost.Find(id);
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
            post.Author = await userService.GetCurrentUser();

            dbPost.Add(post);
            blogContext.SaveChanges();

            return RedirectToAction("Details", new { id = post.Id });
        }

        [HttpGet]
        [Route("/posts/edit/{id:long}")]
        public ActionResult Edit(long id)
        {
            var post = dbPost.Find(id);
            if (post == null)
                return NotFound($"Post {id} not found");

            return View(post);
        }

        [HttpPost]
        [Route("/posts/edit/{id:long}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post updatingPost)
        {
            var post = dbPost.Find(updatingPost.Id);
            if (post == null)
                return NotFound();

            post.Content = updatingPost.Content;
            post.Title = updatingPost.Title;

            blogContext.Entry(post).State = EntityState.Modified;
            blogContext.SaveChanges();
            
            return Redirect("/posts");
        }

        [HttpDelete]
        [Route("/posts/delete/{id:long}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = dbPost.Find(id);
            if (post == null)
                return NotFound($"Post {id} not found");

            blogContext.Entry(post).State = EntityState.Deleted;
            blogContext.SaveChanges();

            return View();
        }
    }
}
