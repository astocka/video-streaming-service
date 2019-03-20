using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public string Name { get; set; }
        [DisplayName("")]
        public int ProfilePictureId { get; set; }
        [DisplayName("")]
        public int AppUserId { get; set; }
    }
}
