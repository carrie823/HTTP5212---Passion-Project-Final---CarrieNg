using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HTTP5212_Passion_Project3.Models
{
    public class Artwork
    {
        [Key]
        public int ArtworkId { get; set; }
        public string ArtworkName { get; set; }
        public string ArtworkDescription { get; set; }

        public string ArtworkImage { get; set; }

        //A artwork belongs to one artist
        //A Artist can have miltiple artworks
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

    }

    public class ArtworkDto
    {
        public int ArtworkId { get; set; }
        public string ArtworkName { get; set; }
        public string ArtworkDescription { get; set; }
        public string ArtworkImage { get; set; }

        public int ArtistName { get; set; }

    }
}