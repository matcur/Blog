using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DataAccess.Models
{
    public class Comment
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public long PostId { get;set; }

        public long AuthodId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Post Post { get; set; }

        public User Autor { get; set; }
    }
}
