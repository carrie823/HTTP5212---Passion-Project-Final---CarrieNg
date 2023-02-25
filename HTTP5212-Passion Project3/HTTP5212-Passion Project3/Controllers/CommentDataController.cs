using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using HTTP5212_Passion_Project3.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace HTTP5212_Passion_Project3.Controllers
{
    public class CommentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// List all information about all comments
        /// </summary>
        /// <example>
        /// localhost/api/CommentData/ListComments ->
        /// CommentId: 6
        /// CommentText: Nice Work
        /// ArtworkId: 6
        /// </example>
        /// <returns>
        /// Returns a list of all the information about all comments
        /// </returns>

        // GET: api/CommentData/ListComments
        [HttpGet]
        public IEnumerable<CommentDto> ListComments()
        {
            List<Comment> Comments = db.Comments.ToList();
            List<CommentDto> CommentDtos = new List<CommentDto>();

            Comments.ForEach(a => CommentDtos.Add(new CommentDto()
            {
                CommentId = a.CommentId,
                CommentText = a.CommentText,
                ArtworkName = a.Artwork.ArtworkId

            }));

            return CommentDtos;
        }

        /// <summary>
        /// Find a comment based on the CommentId provided in the GET request
        /// </summary>
        /// <example>
        /// localhost/api/CommentData/FindComments/6 ->
        /// CommentId: 6
        /// CommentText: Nice Work
        /// ArtworkId: 6
        /// </example>
        /// <returns>
        /// </returns>

        // GET: api/CommentData/FindComment/5
        [ResponseType(typeof(Comment))]
        [HttpGet]
        public IHttpActionResult FindComment(int id)
        {
            Comment Comment = db.Comments.Find(id);
            CommentDto CommentDto = new CommentDto()
            {
                CommentId = Comment.CommentId,
                CommentText = Comment.CommentText,
                ArtworkName = Comment.Artwork.ArtworkId
            };
            if (Comment == null)
            {
                return NotFound();
            }

            return Ok(CommentDto);
        }

        /// <summary>
        /// Update the comment information based on the CommentId specified in the POST request
        /// </summary>
        /// <example>
        /// using a curl request: curl -d @Comment.json -H "Content-type:application/json" localhost/api/CommentData/UpdateComment/6
        /// Before Update:
        /// CommentId: 1
        /// CommentText: I like your art style
        /// ArtworkId: 1
        /// 
        /// After Update:
        /// CommentId: 1
        /// CommentText: I like your art style!!!
        /// ArtworkId: 1
        /// </example>
        /// <returns>
        /// Returns the updated information for the commentId specified in the POST request
        /// </returns>

        // POST: api/CommentData/UpdateComment/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Add a new comment to the Comments Table
        /// using a curl request: curl -d @Artwork.json -H "Content-type:application/json" localhost/api/ArtworkData/UpdateArtwork
        /// </summary>
        /// <returns>
        /// Adds a new comment to the Comments Table
        /// </returns>

        // POST: api/CommentData/AddComment
        [ResponseType(typeof(Comment))]
        [HttpPost]
        public IHttpActionResult AddComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentId }, comment);
        }

        /// <summary>
        /// Deletes a comment from the Comments Table based on the specified CommentId
        /// curl -d "" localhost/api/ArtistData/DeleteArtist/8
        /// </summary>
        /// <returns>
        /// Deletes an artwork from the Comments Table based on the CommentId in the POST request
        /// </returns>

        // POST: api/CommentData/DeleteComment/5
        [ResponseType(typeof(Comment))]
        [HttpPost]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}