using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("MyProfile", "Users");
        }

        public IActionResult MyProfile()
        {
            var session = HttpContext.Session.GetString("Username");
            if(session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        
    }
}