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

namespace Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService postService;

        private readonly UserService userService;

        public PostController(PostService postService, UserService userService)
        {
            this.postService = postService;
            this.userService = userService;
        }

        [HttpGet]
        [Route("/posts")]
        public ActionResult Index(int page = 1, int perPage = 5)
        {
            var postCount = postService.GetCount();
            var pagePosts = postService.GetPaginate(page, perPage);

            ViewBag.PageNavigation = new PageNavigationViewModel(
                    page, postCount / perPage, new UriBuilder($"https://{Request.Host}" + Url.Action("Index"))
                );

            return View(pagePosts);
        }

        [HttpGet]
        [Route("/posts/details/{id:long}")]
        public ActionResult Details(long id)
        {
            var post = postService.FindPostDetail(id);

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
        [ValidateModel]
        [Route("/posts/create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            postService.Create(post);

            return RedirectToAction("Details", new { id = post.Id });
        }

        [HttpGet]
        [Route("/posts/edit/{id:long}")]
        public ActionResult Edit(long id)
        {
            var post = postService.FindPost(id);

            return View(post);
        }

        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        [Route("/posts/edit/{id:long}")]
        public ActionResult Edit(Post updatingPost)
        {
            postService.Update(updatingPost);
            
            return RedirectToAction("Index");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        [Route("/posts/delete/{id:long}")]
        public ActionResult Delete(long id)
        {
            postService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
