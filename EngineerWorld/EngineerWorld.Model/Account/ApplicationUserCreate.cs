using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Model.Account
{
    public class ApplicationUserCreate : ApplicationUserLogin
    {
        [Required(ErrorMessage = "Fullname is required")]
        [MinLength(1, ErrorMessage = "Must be at least 1-30 characters")]
        [MaxLength(30, ErrorMessage = "Must be at least 1-30 characters")]
        public string Fullname { get; set; }

        [MinLength(1, ErrorMessage = "Must be at least 1-30 characters")]
        [MaxLength(30, ErrorMessage = "Must be at least 1-30 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(30, ErrorMessage = "Can be at most 30 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [MaxLength(30, ErrorMessage = "Can be at most 30 characters")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Profession is required")]
        [MinLength(5, ErrorMessage = "Must be at least 5-30 characters")]
        [MaxLength(30, ErrorMessage = "Must be at least 5-30 characters")]
        public string Profession { get; set; }
    }
}
