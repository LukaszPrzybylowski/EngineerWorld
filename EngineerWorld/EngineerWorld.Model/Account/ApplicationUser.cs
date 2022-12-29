using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Model.Account
{
    public class ApplicationUser
    {
        public int ApplicationUserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
