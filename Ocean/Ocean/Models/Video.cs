using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public int YearReleased { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string VideoPath { get; set; }

        public List<CategoryVideo> CategoryVideo { get; set; }
        public List<ActorVideo> ActorVideo { get; set; }
    }
}
