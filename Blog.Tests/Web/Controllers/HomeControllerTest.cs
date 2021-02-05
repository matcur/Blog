using Blog.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Tests.Web.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexViewNotNull()
        {
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
        }
    }
}
