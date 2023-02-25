using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HTTP5212_Passion_Project3.Models;
using HTTP5212_Passion_Project3.Migrations;
using Artwork = HTTP5212_Passion_Project3.Models.Artwork;
using System.Web.Script.Serialization;

namespace HTTP5212_Passion_Project3.Controllers
{
    public class ArtworkController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ArtworkController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44307/api/artworkdata/");
        }

        // GET: Artwork/List
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of artworks
            //curl https://localhost:44307/api/artworkdata/listartwork

            
            string url = "listartwork";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ArtworkDto> artworks = response.Content.ReadAsAsync<IEnumerable<ArtworkDto>>().Result;
                
            //Debug.WriteLine("Number of artist recieved: ");
            //Debug.WriteLine(artworks.Count());

            return View(artworks);
        }

        // GET: Artwork/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our animal data api to retrieve one artworks
            //curl https://localhost:44307/api/artworkdata/findartwork/{id}

            
            string url = "findartwork/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            ArtworkDto selectedartwork = response.Content.ReadAsAsync<ArtworkDto>().Result;
            ;
            Debug.WriteLine("artist recieved: ");
            //Debug.WriteLine(artworks.Count());
            Debug.WriteLine(selectedartwork.ArtworkName);

            return View(selectedartwork);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Artwork/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Artwork/Create
        [HttpPost]
        public ActionResult Create(Artwork artwork)
        {

            Debug.WriteLine("the json payload is ");
            //Debug.WriteLine(artwork.ArtworkName);
            //objective: add a new artwork into our system using the API
            //curl -H "Content-Type:application/json" -d @artwork.json https://localhost:44307/api/artworkdata/addartwork
            string url = "addartwork";

            
            string jsonpayload = jss.Serialize(artwork);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }

        // GET: Artwork/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "findartwork/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ArtworkDto selectedartwork = response.Content.ReadAsAsync<ArtworkDto>().Result;
            return View(selectedartwork);
        }

        // POST: Artwork/Update/5
        [HttpPost]
        public ActionResult Edit(int id, Artwork artwork)
        {
            string url = "updateartwork/"+id;
            string jsonpayload = jss.Serialize(artwork);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Artwork/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findartwork/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ArtworkDto selectedartwork = response.Content.ReadAsAsync<ArtworkDto>().Result;
            return View(selectedartwork);
        }

        // POST: Artwork/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "deleteartwork/"+id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
