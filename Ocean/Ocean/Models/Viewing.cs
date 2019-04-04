using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ocean.Models
{
    public class Viewing
    {
        public int ViewingId { get; set; }
        public DateTime DateView { get; set; }

        public int VideoId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        public int UserProfileId { get; set; }
        [ForeignKey("UserProfileId")]
        public UserProfile UserProfile { get; set; }
    }
}
