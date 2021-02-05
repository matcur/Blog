using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DataAccess.Models
{
    public class Post
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string ThumbnailPath { get; set; }

        public long AuthorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public ApplicationUser Author { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
