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
            client.BaseAddress = new Uri("http://192.168.128.233:1708/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODUyNzQwNDEsImlzcyI6ImJvb3RjYW1wcmVzb3VyY2VtYW5hZ2VtZW50IiwiYXVkIjoicmVhZGVycyJ9.YA-M_KN25FWmUuIS1bd9F5ioiRkVY8NCas1Bjma8kjQ");
            lokal.BaseAddress = new Uri("https://localhost:44397/Login/ResetPassword");
        }

        public ActionResult Index()
        {
            var _username = HttpContext.Session.GetString("Username");
            var _Token = HttpContext.Session.GetString("Token");
            if (_username != null || _Token !=null )
            {
                return RedirectToAction("MyProfile", "Users");
            }
            return View();
        }

        public ActionResult Login(UserVM userVM)
        {
            var myContent = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var login = client.PostAsync("Login", ByteContent).Result;

            if (login.IsSuccessStatusCode)
            {
                var content = login.Content.ReadAsStringAsync().Result.Replace("\"", "").Split("...");
                var Token = "Bearer " + content[0];
                var Username = content[1];
                var Id = content[2];
                HttpContext.Session.SetString("Username", Username);
                HttpContext.Session.SetString("Token", Token);
                HttpContext.Session.SetString("UserId", Id);
                return Json(new { data = content, statusCode=200});
                //return Json(new { data = login });
            }
            return Json(new { statusCode = 400 });

        }
        [HttpPost]
        public async Task<ActionResult> ForgetPassword(UserVM userVM)
        {
            var myContent = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var token = await client.PostAsync("GenerateToken", ByteContent);

            if (token.IsSuccessStatusCode)
            {
                var content = token.Content.ReadAsAsync<UserVM>().Result;
                return Json(new { data = lokal.BaseAddress + "/" + content.Token, Token= content.Token, statusCode = 200 });
                //return Json(new { data = login });
            }
            return Json(new { statusCode = 400 });
        }
        
        public IActionResult ResetPassword(string Token)
        {
            var cek = client.GetAsync("GetToken/?Token=" + Token).Result;
            var read = cek.Content.ReadAsAsync<UserVM>().Result;
            return View(Json(new { data=read}));
        }

      

        [HttpPut("{Token}")]
        public async Task<ActionResult> ResetPasswordAPI( string Token, UserVM userVM)
        {        
                
            var put = await client.PutAsJsonAsync("ForgotPassword/?Token=" +Token, userVM.PasswordHash);
            if (put.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Login");
            }
            return BadRequest("Reset Password Failed");
               
        }


    }
}