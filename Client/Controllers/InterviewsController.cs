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
        private readonly HttpClient server = new HttpClient();
        public InterviewsController()
        {
            client.BaseAddress = new Uri("https://localhost:44306/api/");
            server.BaseAddress = new Uri("http://192.168.128.233:1708/");
            server.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODUyNzQwNDEsImlzcyI6ImJvb3RjYW1wcmVzb3VyY2VtYW5hZ2VtZW50IiwiYXVkIjoicmVhZGVycyJ9.YA-M_KN25FWmUuIS1bd9F5ioiRkVY8NCas1Bjma8kjQ");
        }
        public IActionResult Index()
        {
            ViewBag.Sites = ListSites();
            ViewBag.Employees = ListParticipant();
            ViewBag.Interviews = ListInterview();
           

            return View();
        }

        //admin
        [HttpGet]
        public ActionResult Approve()
        {
            ViewBag.WaitingResult = WaitingResult();
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
        //public IList<ParticipantVM> ListParticipant()
        //{
        //    IList<ParticipantVM> emp = null;
        //    var responseTask = server.GetAsync("Get");
        //    responseTask.Wait();
        //    var result = responseTask.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var readTask = result.Content.ReadAsAsync<IList<ParticipantVM>>();
        //        readTask.Wait();
        //        emp = readTask.Result;
        //    }
        //    return emp;
        //}

        public IList<EmployeeVM> ListParticipant()
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
        public async Task<JsonResult> GetNameParticipant(string ParticipantId)
        {
            EmployeeVM emp = new EmployeeVM();
            var response = await client.GetAsync("Employees/GetById/" + ParticipantId);
            var apiResponse = await response.Content.ReadAsStringAsync();
            return Json(new { data = apiResponse, statusCode = 200 });
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
        public IList<InterviewVM> WaitingResult()
        {
            var all = ListInterview();
            var listwaiting = new List<InterviewVM>();
            foreach(var i in all)
            {
                if (i.Status == "Waiting Result")
                {
                    listwaiting.Add(i);
                }
            }
            return listwaiting;
        }
    }
}