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
        private readonly IUserInterface _helper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor, IUserInterface Helper)
        {
            _httpContextAccessor = httpContextAccessor;
            _helper = Helper;
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
        public IActionResult Login(LoginModel user)
        {
            if (_helper.Login(user))
            {

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "User");
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