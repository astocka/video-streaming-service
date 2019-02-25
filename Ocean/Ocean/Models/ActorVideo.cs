using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean.Models
{
    public class ActorVideo
    {
        public int ActorVideoId { get; set; }
        public int ActorId { get; set; }
        public int VideoId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actors { get; set; }
        [ForeignKey("VideoId")]
        public Video Videos { get; set; }
    }
}
