using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HTTP5212_Passion_Project3.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        //A comment belongs to one artwork
        //A artwork has many comments
        [ForeignKey("Artwork")]
        public int ArtworkId { get; set; }
        public virtual Artwork Artwork { get; set; }
    }

    public class CommentDto
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        public int ArtworkName { get; set; }
    }
}