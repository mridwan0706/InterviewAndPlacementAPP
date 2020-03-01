using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Models;
using Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            ViewBag.Sites = site();
            ViewBag.Employees = employee();
            ViewBag.Interviews = interviewlist();
            return View();
        }

        public IActionResult select()
        {
            return View();
        }
        public IList<SiteVM> site()
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
        }

        public IList<EmployeeVM> employee()
        {
            IList<EmployeeVM> emp = null;
            var responseTask = client.GetAsync("Employees");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<EmployeeVM>>();
                readTask.Wait();
                emp = readTask.Result;
            }
            return emp;
        }

        public IList<InterviewVM> interviewlist()
        {
            IList<InterviewVM> interview = null;
            var responseTask = client.GetAsync("Interviews/GetAll");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<InterviewVM>>();
                readTask.Wait();
                interview = readTask.Result;
            }
            return interview;
        }
        //admin
        [HttpPost]
        public async Task<ActionResult> Insert(InterviewVM interviewVM)
        {
            var myContent = JsonConvert.SerializeObject(interviewVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var post = await client.PostAsync("Interviews", ByteContent);
            if (post.IsSuccessStatusCode)
            {
                return Json(new { data = post, statusCode = 200 });
            }
            return Json(new { statusCode = 400 });
        }

        //admin
        [HttpGet]
        public ActionResult Approve()
        {
            ViewBag.Result = WaitingResult();
            return View();
        }
        //admin
        [HttpGet]
        //status Waiting Result
        public IList<InterviewVM> WaitingResult()
        {
            IList<InterviewVM> Result = null;
            string Status = "Waiting";
            var responseTask = client.GetAsync("Interviews/GetInterviewByStatus/"+ Status);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<InterviewVM>>();
                readTask.Wait();
                Result = readTask.Result;
            }
            return Result;
        }



        //client
        [HttpPut]
        public async Task<ActionResult> AcceptInterview(InterviewVM interviewVM)
        {
            var myContent = JsonConvert.SerializeObject(interviewVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var put = await client.PutAsync("Interviews/Status/" + interviewVM.EmployeeId, ByteContent);
            if (put.IsSuccessStatusCode)
            {
                return Json(new { data = put, statusCode = 200 });
            }
            return Json(new { statusCode = 400 });

        }


    }
}