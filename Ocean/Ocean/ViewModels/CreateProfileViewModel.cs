using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean.ViewModels
{
    public class CreateProfileViewModel
    {
        [Required]
        public string Name { get; set; }
        [DisplayName("Profile Image")]
        public int ProfilePictureId { get; set; }
        public int AppUserId { get; set; }
    }
}
