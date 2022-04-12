using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.EN.Models.User
{
    public class UserPostModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
