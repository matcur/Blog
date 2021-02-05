using Blog.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
{
    public class BlogFile
    {
        private readonly string saveFolder = "/files";

        private readonly IWebHostEnvironment environment;

        public BlogFile(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public string Save(IFormFile file)
        {
            var path = $"{saveFolder}/{file.GetRandomName()}";
            using (var stream = new FileStream(environment.WebRootPath + path, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            return path;
        }
    }
}
