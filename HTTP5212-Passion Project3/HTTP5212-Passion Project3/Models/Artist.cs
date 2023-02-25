using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HTTP5212_Passion_Project3.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }

    public class ArtistDto
    {
        [Key]
        public int ArtistId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}