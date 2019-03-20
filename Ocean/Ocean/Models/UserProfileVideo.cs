using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ocean.Models
{
    public class UserProfileVideo
    {
        public int UserProfileVideoId { get; set; }
        public int UserProfileId { get; set; }
        public int VideoId { get; set; }
        [ForeignKey("UserProfileId")]
        public UserProfile UserProfile { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }
    }
}
