using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DataAccess.Models
{
    public class OAuthAccount
    {
        [ForeignKey("User")]
        public long Id { get; set; }

        public ApplicationUser User { get; set; }
    }
}
