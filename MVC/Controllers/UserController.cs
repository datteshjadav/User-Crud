using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using WebApi.Repositories;


namespace MVC.Controllers
{


    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserInterface _UserHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor, IUserInterface Helper)
        {
            _httpContextAccessor = httpContextAccessor;
            _UserHelper = Helper;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        #region Login Methods

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //Reset to Original working Commit
        public IActionResult Login(LoginModel user)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            if (session.GetInt32("userid") == null)
            {
                if (_UserHelper.Login(user))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }
        [HttpGet]
        public IActionResult SignOut()
        {
            _UserHelper.SignOut();
            return RedirectToAction("Login");
        }

        #endregion


        #region Register Methods

        #endregion








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}