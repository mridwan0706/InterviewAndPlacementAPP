using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Models;
using Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly HttpClient server = new HttpClient();
        public const string _sessionname = "07a7e14c-064a-4f13-a095-011c4acca429";
        public UsersController()
        {
            
            client.BaseAddress = new Uri("https://localhost:44306/api/");           
            server.BaseAddress = new Uri("http://192.168.128.233:1708");
            server.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODUyNzQwNDEsImlzcyI6ImJvb3RjYW1wcmVzb3VyY2VtYW5hZ2VtZW50IiwiYXVkIjoicmVhZGVycyJ9.YA-M_KN25FWmUuIS1bd9F5ioiRkVY8NCas1Bjma8kjQ");
        }
        public ActionResult Index()
        {
            
            return RedirectToAction("MyProfile", "Users");
        }


        public ActionResult MyProfile()
        {
            HttpContext.Session.SetString("Username", _sessionname);
            
            var _username = HttpContext.Session.GetString("Username");
            var _Token = HttpContext.Session.GetString("Token");            
            if (_username == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Users = GetUsers();
            }
            return View();
        }
        //ambil data API Adi untuk Myprofile
        public EmployeeVM GetUsers()
        {
            EmployeeVM users = null;
            //var _id = HttpContext.Session.GetString("UserId");
            var _id = HttpContext.Session.GetString("Username");
            //Akses Server
            //var responseTask = server.GetAsync("Get/" + _id);
            //Coba API SENDIRI
            var responseTask = client.GetAsync("Employees/" + _id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<EmployeeVM>();
                readTask.Wait();
                users = readTask.Result;
            }
            return users;
        } 
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        //tampilan untuk User    
        [Route("Users/ResultInterview")]
        public ActionResult ResultInterview()
        {
            return View();
        }
        public ActionResult Interview()
        {
            HttpContext.Session.SetString("Username", _sessionname);

            var _username = HttpContext.Session.GetString("Username");
            var _Token = HttpContext.Session.GetString("Token");
            if (_username == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.InterviewByUser = InterviewByUserId();
            }
            return View();
        }

        //Client
        public IList<InterviewVM> InterviewByUserId()
        {
            IList<InterviewVM> ListInterview = null;
            var ParticipantId = HttpContext.Session.GetString("Username");
            var responseTask = client.GetAsync("Interviews/GetByUserId/" + ParticipantId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<InterviewVM>>();
                readTask.Wait();
                ListInterview = readTask.Result;
                var Today = DateTime.Now.ToLocalTime();
                foreach(var interview in ListInterview)
                {
                    var interval = Today - interview.CreateDate;
                    if(interval.Days > 30)
                    {
                        interview.Interval = (interval.Days / 30).ToString() + " Months Ago" ;
                    }if(interval.Days < 1)
                    {
                        interview.Interval = interval.Hours.ToString() + " Hours Ago";
                    }
                    //intrview.Interval = intrval.Days;
                }
                
            }
            return ListInterview;
        }
        public async Task<JsonResult> GetDetailInterview(int Id)
        {
            InterviewVM interview = new InterviewVM();
            var response = await client.GetAsync("Interviews/GetAllByInterviewId/" + Id);
            var apiResponse = await response.Content.ReadAsStringAsync();
            return Json(new { data = apiResponse, statusCode = 200 });
        }


        //Change Status
        //[HttpPut]
        //public JsonResult UpdateStatusByInterviewId(int Id, InterviewVM interviewVM)
        //{
        //    var myContent = JsonConvert.SerializeObject(interviewVM);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var ByteContent = new ByteArrayContent(buffer);
        //    ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    var put =  client.PutAsync("Interviews/UpdateStatus/" +Id, ByteContent).Result;
        //    if (put.IsSuccessStatusCode)
        //    {
        //        return Json(new { data = put, statusCode = 200 });
        //    }
        //    return Json(new { statusCode = 400 });
        //}

        public async Task<IActionResult> UpdateStatus(int Id, Interview interview)
        {
            var responseTask = await client.GetAsync("Interviews/" + Id);
            var _interview = await responseTask.Content.ReadAsAsync<InterviewVM>();
            _interview.Status = interview.Status;           
            var myContent = JsonConvert.SerializeObject(_interview);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var putTask = client.PutAsync("Interviews/" +Id, ByteContent).Result;
            
            return Json(new { data=putTask, statusCode=200 });
        }

    }
}