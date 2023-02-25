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
//using Artwork = HTTP5212_Passion_Project3.Models.Artwork;
//using HTTP5212_Passion_Project3.Migrations;
using HTTP5212_Passion_Project3.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace HTTP5212_Passion_Project3.Controllers
{
    public class ArtworkDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// List all information about all artworks
        /// </summary>
        /// <example>
        /// localhost/api/ArtworktData/ListWorks -> 
        /// ArtworkId: 1 
        /// ArtworkName: Ramen Painting
        /// ArtDescription: Drawing of a ramen bowl
        /// ArtistId: 1
        /// ArtworkImage: ramen.jpeg
        /// </example>
        /// <returns>
        /// List all information about all arworks
        /// </returns>

        // GET: api/ArtworkData/ListArtwork
        [HttpGet]
       public IEnumerable<ArtworkDto> ListArtwork()
       {
          List<Artwork> Artworks = db.Artworks.ToList();
          List<ArtworkDto> ArtworkDtos = new List<ArtworkDto>();

           Artworks.ForEach(a => ArtworkDtos.Add(new ArtworkDto()
           {

              ArtworkId = a.ArtworkId,
              ArtworkName = a.ArtworkName,
              ArtworkDescription = a.ArtworkDescription,
              ArtworkImage = a.ArtworkImage,
              ArtistName = a.Artist.ArtistId
           }));

           return ArtworkDtos;
       }

        /// <summary>
        /// Find information on an artwork based on their ArtworkId
        /// </summary>
        /// <example>
        /// localhost/api/ArtworktData/FindWorks/1 -> 
        /// ArtworkId: 1 
        /// ArtworkName: Ramen Painting
        /// ArtDescription: Drawing of a ramen bowl
        /// ArtistId: 1
        /// ArtworkImage: ramen.jpeg
        /// </example>
        /// <returns>
        /// find and list all information on arwork specificed in GET request
        /// </returns>

        // GET: api/ArtworkData/FindArtwork/5
        [ResponseType(typeof(Artwork))]
        [HttpGet]
        public IHttpActionResult FindArtwork(int id)
        {
            Artwork Artwork = db.Artworks.Find(id);
            ArtworkDto ArtworkDto = new ArtworkDto()
            {
                ArtworkId = Artwork.ArtworkId,
                ArtworkName = Artwork.ArtworkName,
                ArtworkDescription = Artwork.ArtworkDescription,
                ArtworkImage = Artwork.ArtworkImage,
                ArtistName = Artwork.Artist.ArtistId
            };
            if (Artwork == null)
            {
                return NotFound();
            }

            return Ok(ArtworkDto);
        }

        /// <summary>
        /// Update artwork information based on the ArtworkId.
        /// </summary>
        /// <example>
        /// using a curl request: curl -d @Artwork.json -H "Content-type:application/json" localhost/api/ArtworkData/UpdateArtwork/7
        /// Before Update:
        /// ArtworkId: 7
        /// ArtworkName: Animal Skull
        /// ArtDescription: Charcoal drawing of an animal skull
        /// ArtistId: 1
        /// ArtworkImage: skull.jpeg
        /// 
        /// After Update:
        /// ArtworkId: 7
        /// ArtworkName: Sheep Skull
        /// ArtDescription: Charcoal drawing of an sheep skull
        /// ArtistId: 1
        /// ArtworkImage: skeep.jpep
        /// </example>
        /// <returns>
        /// updates artwork information based on the ArtworkId
        /// </returns>


        // POST: api/ArtworkData/UpdateArtwork/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateArtwork(int id, Artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artwork.ArtworkId)
            {
                return BadRequest();
            }

            db.Entry(artwork).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtworkExists(id))
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
        /// Adds a new artwork to the Artworks table
        /// using a curl request: curl -d @Artwork.json -H "Content-type:application/json" localhost/api/ArtworkData/UpdateArtwork
        /// </summary>
        /// <returns>
        /// Adds the a new artworks information into the Artworks table
        /// </returns>

        // POST: api/ArtworkData/AddArtwork
        [HttpPost]
        [ResponseType(typeof(Artwork))]
        public IHttpActionResult AddArtwork(Artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artworks.Add(artwork);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artwork.ArtworkId }, artwork);
        }

        /// <summary>
        /// Deletes an artwork from the Artworks table based on the ArtworkId
        /// curl -d "" localhost/api/ArtistData/DeleteArtist/8
        /// </summary>
        /// <returns>
        /// Deletes an artwork from the Artwork Table based on the ArtworkId in the POST request
        /// </returns>

        // POST: api/ArtworkData/DeleteArtwork/5
        [ResponseType(typeof(Artwork))]
        [HttpPost]
        public IHttpActionResult DeleteArtwork(int id)
        {
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return NotFound();
            }

            db.Artworks.Remove(artwork);
            db.SaveChanges();

            return Ok(artwork);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtworkExists(int id)
        {
            return db.Artworks.Count(e => e.ArtworkId == id) > 0;
        }
    }
}