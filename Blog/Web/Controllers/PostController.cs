using Blog.DataAccess;
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
using Blog.Extensions;
using Blog.Core.FilterAttributes.Actions;
using Blog.Core.FilterAttributes.Action;

namespace Blog.Web.Controllers
{
    public class PostController : BlogController
    {
        public PostController(BlogContext blogContext, UserService userService) : base(blogContext, userService) { }

        [HttpGet]
        [Route("/posts")]
        public ActionResult Index(int page = 1)
        {
            var postCount = postTable.Count();
            var posts = postTable.Paginate(page, 2).ToList();

            ViewBag.PageNavigation = new PageNavigationViewModel(
                page, postCount / 2, new UriBuilder($"https://{Request.Host}" + Url.Action("Index"))
                );

            return View(posts);
        }

        [HttpGet]
        [Route("/posts/details/{id:long}")]
        public ActionResult Details(long id)
        {
            var post = postTable.Include(p => p.Comments)
                                .ThenInclude(c => c.Autor)
                                .First(p => p.Id == id);

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
        [Authorize]
        [PostCreating]
        [ValidateModel]
        [Route("/posts/create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            blogContext.Entry(post).State = EntityState.Added;
            postTable.Add(post);
            blogContext.SaveChanges();

            return RedirectToAction("Details", new { id = post.Id });
        }

        [HttpGet]
        [Route("/posts/edit/{id:long}")]
        public ActionResult Edit(long id)
        {
            var post = postTable.First(p => p.Id == id);

            return View(post);
        }

        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        [Route("/posts/edit/{id:long}")]
        public ActionResult Edit(Post updatingPost)
        {
            var post = postTable.First(p => p.Id == updatingPost.Id);

            post.Content = updatingPost.Content;
            post.Title = updatingPost.Title;

            blogContext.Entry(post).State = EntityState.Modified;
            blogContext.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        [Route("/posts/delete/{id:long}")]
        public ActionResult Delete(int id)
        {
            var post = postTable.First(p => p.Id == id);

            blogContext.Entry(post).State = EntityState.Deleted;
            blogContext.SaveChanges();

            return View();
        }
    }
}
