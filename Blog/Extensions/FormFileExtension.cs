using Blog.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Extensions
{
    public static class FormFileExtension
    {
        public static string GetRandomName(this IFormFile file)
        {
            return $"{Randomizer.GetString(8)}.{ Path.GetExtension(file.FileName)}";
        }
    }
}
