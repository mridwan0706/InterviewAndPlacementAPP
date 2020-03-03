using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Client.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            ViewBag.Sites = List();
            return View();
        }

        [HttpGet]
        public IList<SiteVM> List()
        //public JsonResult List()
        {
            IList<SiteVM> sites = null;
            var responseTask = client.GetAsync("Sites");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<SiteVM>>();
                readTask.Wait();
                sites = readTask.Result;
            }
            return sites;
            //return Json(new { data = sites });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SiteVM site)
        {
            var context = JsonConvert.SerializeObject(site);
            var buffer = System.Text.Encoding.UTF8.GetBytes(context);
            var byteContext = new ByteArrayContent(buffer);
            byteContext.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var postTask = await client.PostAsync("Sites", byteContext);           
            return Json(new { data = postTask });
        }

        [HttpPost]
        public async Task<JsonResult> Upload(IFormFile logo)
        {
            if (logo == null || logo.Length == 0)
            {
                return Json(null);
            }
            else
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", logo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await logo.CopyToAsync(stream);
                return Json(logo.FileName);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, SiteVM site)
        {
            var context = JsonConvert.SerializeObject(site);
            var buffer = System.Text.Encoding.UTF8.GetBytes(context);
            var byteContext = new ByteArrayContent(buffer);
            byteContext.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var putTask = await client.PutAsync("Sites/" + id, byteContext);
            return Json(new { data = putTask });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteTask = await client.DeleteAsync("Sites/" + id);
            return Json(new { data = deleteTask });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var responseTask = await client.GetAsync("Sites/" + id);
            var site = await responseTask.Content.ReadAsAsync<SiteVM>();
            return Json(new { data = site });
        }
    }
}