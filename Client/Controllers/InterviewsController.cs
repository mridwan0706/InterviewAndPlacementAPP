using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Models;
using Client.ViewModels;
using Microsoft.AspNetCore.Http;
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
            ViewBag.Sites = ListSites();
            ViewBag.Employees = ListEmployee();
            ViewBag.Interviews = ListInterview();
            ViewBag.Ready = WaitingInterview();

            return View();
        }
       //Show DropDown in Modal Invitation Interview
        public IList<SiteVM> ListSites()
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
        public IList<UserVM> ListEmployee()
        {
            IList<UserVM> emp = null;
            var responseTask = client.GetAsync("Employees");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<UserVM>>();
                readTask.Wait();
                emp = readTask.Result;
            }
            return emp;
        }
        //admin - Join TB_M_Sites, TB_M_Interviews, TB_M_Employee
        public IList<InterviewVM> ListInterview()
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
        //Invitation Interview
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
            ViewBag.Ready = WaitingResult();
            return View();
        }
        //admin
        [HttpGet]
        //Get status Ready Interview 
        public IList<InterviewVM> WaitingInterview()
        {
            IList<InterviewVM> Ready = null;
            string Status = "Ready Interview";
            var responseTask = client.GetAsync("Interviews/GetInterviewByStatus/"+ Status);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<InterviewVM>>();
                readTask.Wait();
                Ready = readTask.Result;
            }
            return Ready;
        }

        public IList<InterviewVM> WaitingResult()
        {
            IList<InterviewVM> WaitingResult = null;
            string Status = "Waiting Result";
            var responseTask = client.GetAsync("Interviews/GetInterviewByStatus/" + Status);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<InterviewVM>>();
                readTask.Wait();
                WaitingResult = readTask.Result;
            }
            return WaitingResult;
        }
        //Client
        public IList<InterviewVM> ListInterviewByEmployeeId()
        {
            IList<InterviewVM> interview = null;
            var EmployeeId = HttpContext.Session.GetString("Username");
            var responseTask = client.GetAsync("Interviews/GetAllByEmployeeId/" + EmployeeId);
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


    }
}