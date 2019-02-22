using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean.Models
{
    public class ProfilePicture
    {
        public int ProfilePictureId { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public string ThumbnailFilePath { get; set; }

        public List<UserProfile> UserProfiles { get; set; }
       
    }
}
