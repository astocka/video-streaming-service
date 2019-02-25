using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public List<ActorVideo> ActorVideo { get; set; }
    }
}
