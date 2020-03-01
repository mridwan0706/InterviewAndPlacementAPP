using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Client.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        public UsersController()
        {
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("Id", "1");
            return RedirectToAction("MyProfile", "Users");
           
        }

        public IActionResult MyProfile()
        {
            var session = HttpContext.Session.GetString("Username");
            //if(session == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            return View();
        }
              
        public IActionResult Detail()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

       
    }
}