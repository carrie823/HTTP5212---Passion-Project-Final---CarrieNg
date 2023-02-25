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
using System.Diagnostics;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace HTTP5212_Passion_Project3.Controllers
{
    public class ArtistDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// List all information about artists
        /// </summary>
        /// <example>
        /// localhost/api/ArtistData/ListArtists -> ArtistId: 1, FirstName: Emily, LastName: Parker
        /// </example>
        /// <returns>
        /// List all information about artists
        /// </returns>

        // GET: api/ArtistData/ListArtists
        [HttpGet]
        public IEnumerable<ArtistDto> ListArtists()
        {
            List<Artist> Artists = db.Artists.ToList();
            List<ArtistDto> ArtistDtos = new List<ArtistDto>();

            Artists.ForEach(a => ArtistDtos.Add(new ArtistDto()
            {
                ArtistId = a.ArtistId,
                FirstName = a.FirstName,
                LastName = a.LastName,
            }));

            return ArtistDtos;
        }

        /// <summary>
        /// Find artist based on their ArtistId and displays their information
        /// </summary>
        /// <example>
        /// localhost/api/ArtistData/FindArtist/3 -> ArtistId: 3, FirstName: Jessica, LastName: Lee
        /// </example>
        /// <returns>
        /// Returns all information on artist based on their ArtistId
        /// </returns>
        
        // GET: api/ArtistData/FindArtist/5

        [ResponseType(typeof(Artist))]
        [HttpGet]
        public IHttpActionResult FindArtist(int id)
        {
            Artist Artist = db.Artists.Find(id);
            ArtistDto ArtistDto = new ArtistDto()
            {
                ArtistId = Artist.ArtistId,
                FirstName = Artist.FirstName,
                LastName = Artist.LastName,
            };
            if (Artist == null)
            {
                return NotFound();
            }

            return Ok(ArtistDto);
        }

        /// <summary>
        /// Update artist information based on their ArtistId. 
        /// </summary>
        /// <example>
        /// using a curl request: curl -d @Artist.json -H "Content-type:application/json" localhost/api/ArtistData/UpdateArtist/5
        /// Before Update:
        /// ArtistId: 5 
        /// FirstName: Lia
        /// LastName: Chan
        /// 
        /// After Update
        /// ArtistId: 5
        /// FirstName: Mia
        /// LastName: Chan
        /// </example>
        /// <returns>
        /// Returns the updated information of artist based on their ArtistId
        /// </returns>


        // POST: api/ArtistData/UpdateArtist/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateArtist(int id, Artist artist)
        {
            Debug.WriteLine("Artist Update Method");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            db.Entry(artist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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
        /// Add an artist to the Artists table 
        /// using a curl request: curl -d @Artist.json -H "Content-type:application/json" localhost/api/ArtistData/AddArtist/
        /// </summary>
        /// <returns>
        /// Adds artist information to the Artists table 
        /// </returns>

        // POST: api/ArtistData/AddArtist
        [ResponseType(typeof(Artist))]
        [HttpPost]
        public IHttpActionResult AddArtist(Artist artist)
           
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artists.Add(artist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artist.ArtistId }, artist);
        }

        /// <summary>
        /// Deletes an artist from the artist table based on their ArtistId 
        /// curl -d "" localhost/api/ArtistData/DeleteArtist/5
        /// </summary>
        /// <returns>
        /// Deletes the artist from the artist table based on the ArtistId specified in the POST request
        /// </returns>

        // POST: api/ArtistData/DeleteArtist/5
        [ResponseType(typeof(Artist))]
        [HttpPost]
        public IHttpActionResult DeleteArtist(int id)
        {
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return NotFound();
            }

            db.Artists.Remove(artist);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtistExists(int id)
        {
            return db.Artists.Count(e => e.ArtistId == id) > 0;
        }
    }
}