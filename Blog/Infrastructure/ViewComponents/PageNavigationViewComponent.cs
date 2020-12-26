using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ViewComponents
{
    public class PageNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PageNavigationViewModel pageNavigation)
        {
            var htmlNavigation = ParseHtmlNavigation(pageNavigation);

            return new HtmlContentViewComponentResult(
                new HtmlString(htmlNavigation)
            );
        }

        private string ParseHtmlNavigation(PageNavigationViewModel pageNavigation)
        {
            var result = "<div>";
            for (int i = 1; i <= pageNavigation.MaxPageNumber; i++)
                result += GetAnchorByPageNumber(pageNavigation, i);
            
            result += "</div>";

            return result;
        }

        private string GetAnchorByPageNumber(PageNavigationViewModel pageNavigation, int i)
        {
            return $"<a href=\"{pageNavigation.GetLinkByPageNumber(i)}\">{i}</a>";
        }
    }
}
