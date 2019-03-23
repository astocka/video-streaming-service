using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ocean.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public Rate Rate { get; set; }

        public int UserProfileId { get; set; }
        [ForeignKey("UserProfileId")]
        public UserProfile UserProfile { get; set; }

        public List<VideoRating> VideoRating { get; set; }
        
    }
}
