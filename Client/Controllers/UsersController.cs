using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        public const string _sessionname = "1";
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
        
        public UserVM GetUsers()
        {
           UserVM users = null;
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
                var readTask = result.Content.ReadAsAsync<UserVM>();
                readTask.Wait();
                users = readTask.Result;


            }
            return users;
        }

        //public JsonResult GetUserSession()
        //{
        //    var _id = HttpContext.Session.GetString("UserId");
        //    var cek = server.GetAsync("Get/" + _id).Result;
        //    var read = cek.Content.ReadAsAsync<UserVM>().Result;
        //    return Json(new { data = read });
        //}





        public IActionResult Detail()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Interview()
        {
            return View();
        }

        
        
    }
}