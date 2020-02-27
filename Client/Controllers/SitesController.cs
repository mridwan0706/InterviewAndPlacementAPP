using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.ViewModels;
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
            return View(List());
        }

        [HttpGet]
        public JsonResult List()
        {
            IEnumerable<SiteVM> sites = null;
            var responseTask = client.GetAsync("Sites");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<SiteVM>>();
                readTask.Wait();
                sites = readTask.Result;
            }
            else
            {
                sites = Enumerable.Empty<SiteVM>();
                ModelState.AddModelError(string.Empty, "404 Not Found");
            }
            return Json(new { data = sites });
        }

        [HttpPost]
        public JsonResult Create(SiteVM interview)
        {
            var postTask = client.PostAsJsonAsync("Sites", interview).ToString();
            return Json(new { data = postTask });
        }

        [HttpPut]
        public JsonResult Edit(int id, SiteVM site)
        {
            var putTask = client.PutAsJsonAsync("Sites/" + id, site).ToString();
            return Json(new { data = putTask });
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var deleteTask = client.DeleteAsync("Sites/" + id);
            return Json(new { data = deleteTask });
        }

        [HttpGet]
        public JsonResult Detail(int id)
        {
            var responseTask = client.GetAsync("Sites/" + id).Result.Content.ReadAsAsync<SiteVM>().Result;
            return Json(new { data = responseTask });
        }
    }
}