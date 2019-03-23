using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ocean.Models
{
    public class VideoRating
    {
        public int VideoRatingId { get; set; }
        public int VideoId { get; set; }
        public int RatingId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }
        [ForeignKey("RatingId")]
        public Rating Rating { get; set; }
    }
}
