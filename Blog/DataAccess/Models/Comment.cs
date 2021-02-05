using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DataAccess.Models
{
    public class Comment
    {
        public long Id { get; set; }

        [Required]
        public string Content { get; set; }

        public long PostId { get;set; }

        public long AuthorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Post Post { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
