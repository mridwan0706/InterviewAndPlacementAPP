using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        readonly HttpClient client = new HttpClient();

        public LoginController()
        {
            client.BaseAddress = new Uri("http://192.168.128.79:1708/API/");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(UserVM userVM)
        {
            var myContent = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var login = client.PostAsync("Users/Login", ByteContent).Result;
            if (login.IsSuccessStatusCode)
            {
                var content = login.Content.ReadAsStringAsync().Result.Replace("\"", "").Split("...");
                var token = "Bearer " + content[0];
                var username = content[1];
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Token", token);
                return Json(new { data = content});
                //return Json(new { data = login });
            }
            return BadRequest();

        }


    }
}