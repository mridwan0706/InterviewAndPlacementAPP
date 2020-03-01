using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class SitesController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        public SitesController()
        {
            client.BaseAddress = new Uri("https://localhost:44306/api/");            
        }
        public IActionResult Index()
        {
            ViewBag.Sites = site();
            return View();
        }

        public IList<Site> site()
        {
            IList<Site> sites = null;
            var responseTask = client.GetAsync("Sites");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Site>>();
                readTask.Wait();
                sites = readTask.Result;
            }
            return sites;
        }
    }
}