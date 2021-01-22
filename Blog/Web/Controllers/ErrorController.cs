using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Details(int code)
        {
            ViewBag.ErrorCode = code;

            return View();
        }
    }
}
