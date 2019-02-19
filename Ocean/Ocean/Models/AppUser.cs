using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ocean.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string ProfilePicture { get; set; }

        public AppUser(string userName) : base(userName)
        {

        }

        public AppUser()
        {
        }
    }
}
