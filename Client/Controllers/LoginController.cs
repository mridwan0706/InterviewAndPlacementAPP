using System;
using System.Collections.Generic;
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
    public class LoginController : Controller
    {
        readonly HttpClient client = new HttpClient();
        readonly HttpClient lokal = new HttpClient();

        public LoginController()
        {
            client.BaseAddress = new Uri("http://192.168.128.79:1708/API/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODUyNzQwNDEsImlzcyI6ImJvb3RjYW1wcmVzb3VyY2VtYW5hZ2VtZW50IiwiYXVkIjoicmVhZGVycyJ9.YA-M_KN25FWmUuIS1bd9F5ioiRkVY8NCas1Bjma8kjQ");
            lokal.BaseAddress = new Uri("https://localhost:44397/Login");
        }

        public ActionResult Index()
        {
            var session = HttpContext.Session.GetString("Username");
            if (session != null)
            {
                return RedirectToAction("MyProfile", "Users");
            }
            return View();
        }

        public async Task<ActionResult> Login(UserVM userVM)
        {
           

            var myContent = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var login = await client.PostAsync("Users/Login", ByteContent);
            
            if (login.IsSuccessStatusCode)                
            {
                var content = login.Content.ReadAsStringAsync().Result.Replace("\"", "").Split("...");
                var token = "Bearer " + content[0];
                var username = content[1];
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Token", token);
                return Json(new { data = content, statusCode = 200});
                //return Json(new { data = login });
            }
            return Json(new {statusCode = 400 });

        }
        [HttpPost]
        public async Task<ActionResult> ForgetPassword(UserVM userVM)
        {
            var myContent = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var token = await client.PostAsync("Users/GenerateToken", ByteContent);

            if (token.IsSuccessStatusCode)
            {
                var content = token.Content.ReadAsAsync<UserVM>().Result;                         
                return Json(new { data = lokal.BaseAddress+"/"+content.Id, statusCode=200});
                //return Json(new { data = login });
            }
            return Json(new { statusCode = 400 });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ResetPassword(string id, string passwordnew)
        {
            var user = client.GetAsync("Users/"+id).Result.Content.ReadAsAsync<UserVM>().Result;
            user.PasswordHash = passwordnew;
            var put = await client.PutAsJsonAsync("Users/ForgetPassword/"+user.Id, user);
            return View(put);
        }


    }
}