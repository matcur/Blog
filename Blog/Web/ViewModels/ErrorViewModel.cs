using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.ViewModels
{
    public class ErrorViewModel
    {
        public int Code { get; }

        public string Info => errorInfo[Code];

        private readonly Dictionary<int, string> errorInfo = new Dictionary<int, string>
        {
            { 404, "Page not found." },
            { 500, "Monkey already works." }
        };

        public ErrorViewModel(int code)
        {
            if (!errorInfo.Keys.Contains(code))
                code = 404;

            Code = code;
        }
    }
}
