using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ocean.Models
{
    public class CategoryVideo
    {
        public int CategoryVideoId { get; set; }
        public int CategoryId { get; set; }
        public int VideoId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }
    }
}
