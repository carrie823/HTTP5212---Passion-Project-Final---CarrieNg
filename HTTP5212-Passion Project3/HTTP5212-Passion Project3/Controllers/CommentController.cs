using HTTP5212_Passion_Project3.Migrations;
using HTTP5212_Passion_Project3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HTTP5212_Passion_Project3.Controllers
{
    public class CommentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CommentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44307/api/commentdata/");
        }

        // GET: Comment/List
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of artworks
            //curl https://localhost:44307/api/commentdata/listcomments

            string url = "listcomments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<CommentDto> comments = response.Content.ReadAsAsync<IEnumerable<CommentDto>>().Result;
            //Debug.WriteLine("Number of artists received: ");
            //Debug.WriteLine(artists.Count());

            return View(comments);
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our artist data api, to retrieve one artist
            //curl https://localhost:44307/api/commentdata/findcomment/{id}

            string url = "findcomment/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            CommentDto selectedcomment = response.Content.ReadAsAsync<CommentDto>().Result;
            Debug.WriteLine("comment received: ");
            Debug.WriteLine(selectedcomment.CommentText);

            return View(selectedcomment);

        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Comment/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            Debug.WriteLine("The json payload is ");
            //Debug.WriteLine(comment.CommentText);
            //objective: add a new animal into our system using the api
            //curl -H "Content-Type:application/json" -d @comment.json https://localhost:44307/api/commentdata/addcomment
            string url = "addcomment";


            string jsonpayload = jss.Serialize(comment);

            Debug.WriteLine(comment);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response.ToString());
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "findcomment/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CommentDto selectedartwork = response.Content.ReadAsAsync<CommentDto>().Result;
            return View(selectedartwork);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Comment comment)
        {
            string url = "updateartwork/"+id;
            string jsonpayload = jss.Serialize(comment);
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

        // GET: Comment/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findcomment/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CommentDto selectedcomment = response.Content.ReadAsAsync<CommentDto>().Result;
            return View(selectedcomment);
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "deletecomment/"+id;
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
