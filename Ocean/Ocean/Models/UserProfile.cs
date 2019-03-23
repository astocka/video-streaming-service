using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        [Required]
        public string Name { get; set; }
        public int AppUserId { get; set; }
        public int ProfilePictureId { get; set; }
        public bool? IsActive { get; set; }

        public AppUser AppUser { get; set; }
        public ProfilePicture ProfilePicture { get; set; }

        public List<UserProfileVideo> UserProfileVideo { get; set; }

        public List<Rating> Ratings { get; set; }

    }
}
