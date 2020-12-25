using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords must be same")]
        public string Password { get; set; }

        [Required]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords must be same")]
        public string ConfirmPassword { get; set; }
    }
}
