using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Diagnostics;
using HTTP5212_Passion_Project3.Models;
using System.Web.Script.Serialization;

namespace HTTP5212_Passion_Project3.Controllers
{
    public class ArtistController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ArtistController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44307/api/artistdata/");
        }
        // GET: Artist/List
        public ActionResult List()
        {
            //objective: communicate with our artist data api, to retrieve a list of artists
            //curl https://localhost:44307/api/artistdata/listartists

            string url = "listartists";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ArtistDto> artists = response.Content.ReadAsAsync<IEnumerable<ArtistDto>>().Result;
            //Debug.WriteLine("Number of artists received: ");
            //Debug.WriteLine(artists.Count());

            return View(artists);
        }

        // GET: Artist/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our artist data api, to retrieve one artist
            //curl https://localhost:44307/api/artistdata/findartist/{id}

            string url = "findartist/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            ArtistDto selectedartist = response.Content.ReadAsAsync<ArtistDto>().Result;
            Debug.WriteLine("Artist received: ");
            Debug.WriteLine(selectedartist.FirstName);

            return View(selectedartist);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Artist/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Artist/Create
        [HttpPost]
        public ActionResult Create(Artist artist)
        {
            Debug.WriteLine("The json payload is ");
            //Debug.WriteLine(artist.FirstName);
            //objective: add a new animal into our system using the api
            //curl -H "Content-Type:application/json" -d @artist.json https://localhost:44307/api/artistdata/addartist
            string url = "addartist";


            string jsonpayload = jss.Serialize(artist);

            Debug.WriteLine(artist);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            
            content.Headers.ContentType.MediaType = "application/json";
            
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response.ToString());
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

           
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "findartist/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ArtistDto selectedartist = response.Content.ReadAsAsync<ArtistDto>().Result;
            return View(selectedartist);
        }

        // POST: Artist/Update/5
        [HttpPost]
        public ActionResult Update(int id, Artist artist)
        {
            string url = "updateartist/"+id;
            string jsonpayload = jss.Serialize(artist);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Artist/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findartist/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result; 
            ArtistDto selectedartist = response.Content.ReadAsAsync<ArtistDto>().Result;
            return View(selectedartist);
        }

        // POST: Artist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "deleteartist/"+id;
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
