using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class InterviewsController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        public InterviewsController()
        {
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }

        public IActionResult Index()
        {
            ViewBag.Employees = EmployeeList();
            ViewBag.Sites = SiteList();
            return View(List());
        }

        [HttpGet]
        public JsonResult List()
        {
            IEnumerable<InterviewVM> interviews = null;
            var responseTask = client.GetAsync("Interviews");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<InterviewVM>>();
                readTask.Wait();
                interviews = readTask.Result;
            }
            else
            {
                interviews = Enumerable.Empty<InterviewVM>();
                ModelState.AddModelError(string.Empty, "404 Not Found");
            }
            return Json(new { data = interviews });
        }

        [HttpPost]
        public async Task<JsonResult> Create(InterviewVM interview)
        {            
            var postTask = await client.PostAsJsonAsync("Interviews", interview);
            return Json(new { data = postTask });
        }

        [HttpPut]
        public JsonResult Edit(int id, InterviewVM interview)
        {
            var putTask = client.PutAsJsonAsync("Interviews/" + id, interview).ToString();
            return Json(new { data = putTask });
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var deleteTask = client.DeleteAsync("Interviews/" + id);
            return Json(new { data = deleteTask });
        }

        [HttpGet]
        public JsonResult Detail(int id)
        {
            var responseTask = client.GetAsync("Interviews/" + id).Result.Content.ReadAsAsync<InterviewVM>().Result;
            return Json(new { data = responseTask });
        }

        [HttpGet]
        public IList<EmployeeVM> EmployeeList()
        {
            IList<EmployeeVM> employees = null;
            var responseTask = client.GetAsync("Employees");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<EmployeeVM>>();
                readTask.Wait();
                employees = readTask.Result;
            }
            return employees;
        }

        [HttpGet]
        public IList<SiteVM> SiteList()
        {
            IList<SiteVM> sites = null;
            var responseTask = client.GetAsync("Employees");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<SiteVM>>();
                readTask.Wait();
                sites = readTask.Result;
            }
            return sites;
        }


    }
}